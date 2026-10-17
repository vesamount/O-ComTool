using System.Windows;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Rendering;
using O_ComTool.Core.Text;

namespace O_ComTool.App.Editor
{
    /**
     * @brief 保存接收区里 ANSI 颜色的绝对偏移
     */
    public sealed class ColorRunStore
    {
        private readonly List<ColorRun> _runs = new();

        /**
         * @brief 追加一段新文本上的颜色
         * @param baseOffset: 这段文本在文档中的起始偏移
         * @param runs: 相对偏移的颜色
         */
        public void Add(int baseOffset, IReadOnlyList<ColorRun> runs)
        {
            foreach (ColorRun run in runs)
            {
                if (run.Length <= 0)
                {
                    continue;
                }

                if (run.Foreground == null && run.Background == null && !run.Bold)
                {
                    continue;
                }

                _runs.Add(run with { Start = baseOffset + run.Start });
            }
        }

        /**
         * @brief 清空颜色
         */
        public void Clear()
        {
            _runs.Clear();
        }

        /**
         * @brief 文档替换后修正后续颜色偏移，并丢掉被改写的区间
         * @param offset: 替换起点
         * @param removed: 删除长度
         * @param inserted: 插入长度
         */
        public void ShiftAfterReplace(int offset, int removed, int inserted)
        {
            int delta = inserted - removed;
            for (int i = _runs.Count - 1; i >= 0; i--)
            {
                ColorRun run = _runs[i];
                int end = run.Start + run.Length;
                if (end <= offset)
                {
                    continue;
                }

                if (run.Start >= offset + removed)
                {
                    _runs[i] = run with { Start = run.Start + delta };
                    continue;
                }

                _runs.RemoveAt(i);
            }
        }

        /**
         * @brief 取出与给定区间重叠的颜色
         * @param start: 起点
         * @param length: 长度
         * @retval 重叠的颜色
         */
        public IEnumerable<ColorRun> Overlapping(int start, int length)
        {
            int end = start + length;
            foreach (ColorRun run in _runs)
            {
                if (run.Start < end && run.Start + run.Length > start)
                {
                    yield return run;
                }
            }
        }
    }

    /**
     * @brief 只给可见行上色：先画 ANSI，再用 JSON 规则覆盖前景色
     */
    public sealed class ReceiveColorizer : DocumentColorizingTransformer
    {
        private readonly Dictionary<int, SolidColorBrush> _brushes = new();

        public ColorRunStore Store { get; } = new();

        public HighlightEngine Engine { get; set; } = new();

        public bool HighlightEnabled { get; set; } = true;

        protected override void ColorizeLine(DocumentLine line)
        {
            foreach (ColorRun run in Store.Overlapping(line.Offset, line.Length))
            {
                int start = Math.Max(run.Start, line.Offset);
                int end = Math.Min(run.Start + run.Length, line.EndOffset);
                if (start >= end)
                {
                    continue;
                }

                ChangeLinePart(start, end, element => Apply(element, run.Foreground, run.Background, run.Bold));
            }

            if (!HighlightEnabled)
            {
                return;
            }

            string text = CurrentContext.Document.GetText(line);
            foreach (HighlightMatch match in Engine.Match(text))
            {
                int start = line.Offset + match.Index;
                int end = start + match.Length;
                if (end > line.EndOffset)
                {
                    end = line.EndOffset;
                }

                if (start >= end)
                {
                    continue;
                }

                ChangeLinePart(start, end, element => Apply(element, match.Foreground, null, match.Bold));
            }
        }

        private void Apply(VisualLineElement element, ColorRgba? foreground, ColorRgba? background, bool bold)
        {
            if (foreground is { } fore)
            {
                element.TextRunProperties.SetForegroundBrush(Brush(fore));
            }

            if (background is { } back)
            {
                element.TextRunProperties.SetBackgroundBrush(Brush(back));
            }

            if (bold)
            {
                Typeface typeface = element.TextRunProperties.Typeface;
                element.TextRunProperties.SetTypeface(new Typeface(typeface.FontFamily, typeface.Style, FontWeights.Bold, typeface.Stretch));
            }
        }

        private SolidColorBrush Brush(ColorRgba color)
        {
            int key = color.ToRgb();
            if (_brushes.TryGetValue(key, out SolidColorBrush? brush))
            {
                return brush;
            }

            brush = new SolidColorBrush(Color.FromRgb(color.R, color.G, color.B));
            brush.Freeze();
            _brushes[key] = brush;
            return brush;
        }
    }
}
