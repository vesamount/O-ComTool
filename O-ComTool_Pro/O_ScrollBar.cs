using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace O_ComTool_Pro
{
    #region O_ScrollBar
    public partial class O_ScrollBar : Control
    {
        private int @value;

        public int Value
        {
            get { return value; }
            set
            {
                if (this.value == value)
                    return;
                this.value = value;
                Invalidate();
                OnScroll();
            }
        }

        private int maximum = 100;
        public int Maximum
        {
            get { return maximum; }
            set { maximum = value; Invalidate(); }
        }

        private int thumbSize = 10;
        public int ThumbSize
        {
            get { return thumbSize; }
            set { thumbSize = value; Invalidate(); }
        }

        private Color thumbColor = Color.Gray;
        public Color ThumbColor
        {
            get { return thumbColor; }
            set { thumbColor = value; Invalidate(); }
        }

        private Color borderColor = Color.Silver;
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; Invalidate(); }
        }

        private ScrollOrientation orientation;
        public ScrollOrientation Orientation
        {
            get { return orientation; }
            set { orientation = value; Invalidate(); }
        }

        public event ScrollEventHandler Scroll;

        public O_ScrollBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                MouseScroll(e);
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                MouseScroll(e);
            base.OnMouseMove(e);
        }

        /*
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                OnScroll(ScrollEventType.EndScroll);
            base.OnMouseUp(e);
        }*/

        private void MouseScroll(MouseEventArgs e)
        {
            int v = 0;
            switch (Orientation)
            {
                case ScrollOrientation.VerticalScroll: v = Maximum * (e.Y - thumbSize / 2) / (Height - thumbSize); break;
                case ScrollOrientation.HorizontalScroll: v = Maximum * (e.X - thumbSize / 2) / (Width - thumbSize); break;
            }
            Value = Math.Max(0, Math.Min(Maximum, v));
        }

        public virtual void OnScroll(ScrollEventType type = ScrollEventType.ThumbPosition)
        {
            if (Scroll != null)
                Scroll(this, new ScrollEventArgs(type, Value, Orientation));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Maximum <= 0)
                return;

            Rectangle thumbRect = Rectangle.Empty;
            switch (Orientation)
            {
                case ScrollOrientation.HorizontalScroll:
                    thumbRect = new Rectangle(value * (Width - thumbSize) / Maximum, 2, thumbSize, Height - 4);
                    break;
                case ScrollOrientation.VerticalScroll:
                    thumbRect = new Rectangle(2, value * (Height - thumbSize) / Maximum, Width - 4, thumbSize);
                    break;
            }

            using (var brush = new SolidBrush(thumbColor))
                e.Graphics.FillRectangle(brush, thumbRect);

            using (var pen = new Pen(borderColor))
                e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, Width - 1, Height - 1));
        }
    }

    #endregion
    //public partial class O_ScrollBar : Component
    //{
    //    //public O_ScrollBar()
    //    //{
    //    //    InitializeComponent();
    //    //}

    //    //public O_ScrollBar(IContainer container)
    //    //{
    //    //    container.Add(this);

    //    //    InitializeComponent();
    //    //}

    //}
}
