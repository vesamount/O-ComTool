using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Drawing;

namespace O_ComTool_Pro
{
    public partial class HightLightRichTextBox : System.Windows.Forms.RichTextBox
    {
        private HightLightSettings m_settings = new HightLightSettings();
        private static bool m_bPaint = true;
        private string m_strLine = "";
        private int m_nLineLength = 0;
        private int m_nLineStart = 0;
        private string m_strKeywords = "";

        /// <summary>
        /// The settings.
        /// </summary>
        public HightLightSettings Settings
        {
            get { return m_settings; }
        }

        /// <summary>
        /// WndProc
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 0x00f)
            {
                if (m_bPaint)
                    base.WndProc(ref m);
                else
                    m.Result = IntPtr.Zero;
            }
            else
                base.WndProc(ref m);
        }

        /// <summary>
        /// OnTextChanged
        /// </summary>
        /// <param name="e"></param>
        //protected override void OnTextChanged(EventArgs e)
        //{
        //    if (Settings.Enable == false) return;
        //    // Calculate shit here.
        //    m_nContentLength = this.TextLength;

        //    int nCurrentSelectionStart = SelectionStart;
        //    int nCurrentSelectionLength = SelectionLength;

        //    m_bPaint = false;

        //    // Find the start of the current line.
        //    m_nLineStart = nCurrentSelectionStart;
        //    while ((m_nLineStart > 0) && (Text[m_nLineStart - 1] != '\n'))
        //        m_nLineStart--;
        //    // Find the end of the current line.
        //    m_nLineEnd = nCurrentSelectionStart;
        //    while ((m_nLineEnd < Text.Length) && (Text[m_nLineEnd] != '\n'))
        //        m_nLineEnd++;
        //    // Calculate the length of the line.
        //    m_nLineLength = m_nLineEnd - m_nLineStart;
        //    // Get the current line.
        //    m_strLine = Text.Substring(m_nLineStart, m_nLineLength);

        //    // Process this line.
        //    ProcessLine();

        //    m_bPaint = true;
        //}

        /// <summary>
        /// Process a line.
        /// </summary>
        private void ProcessLine()
        {
            // Save the position and make the whole line black
            int nPosition = SelectionStart;

            SelectionStart = m_nLineStart;
            SelectionLength = m_nLineLength;
            SelectionColor = Settings.DefaultColor;

            // Process the keywords
            ProcessRegex(m_strKeywords, Settings.Keywords, Settings.KeywordColor);
             

            SelectionStart = nPosition;
            SelectionLength = 0;
            SelectionColor = Settings.DefaultColor;
        }

        /// <summary>
        /// Process a regular expression.
        /// </summary>
        /// <param name="strRegex">The regular expression.</param>
        /// <param name="color">The color.</param>
        private void ProcessRegex(string strRegex, List<string> keys, List<Color> color)
        {
            Regex regKeywords = new Regex(strRegex, RegexOptions.Compiled);
            Match regMatch;

            for (regMatch = regKeywords.Match(m_strLine); regMatch.Success; regMatch = regMatch.NextMatch())
            {
                // Process the words
                int nStart = m_nLineStart + regMatch.Index;
                int nLenght = regMatch.Length;
                SelectionStart = nStart;
                SelectionLength = nLenght;
                for (int i = 0; i < color.Count; i++)
                {
                    if (regMatch.Value == keys[i])
                    {
                        SelectionColor = color[i];
                    }
                }  
            }
        }

        /// <summary>
        /// Compiles the keywords as a regular expression.
        /// </summary>
        public void CompileKeywords()
        {
            m_strKeywords = "";
            for (int i = 0; i < Settings.Keywords.Count; i++)
            {
                string strKeyword = Settings.Keywords[i];

                if (i == Settings.Keywords.Count - 1)
                    m_strKeywords += strKeyword;
                else
                    m_strKeywords += strKeyword + "|";
            }
        }
    }

    /// <summary>
    /// Class to store HightLight objects in.
    /// </summary>
    public class HightLightList
    {
        public List<string> m_rgList = new List<string>();
        public List<Color> m_color = new List<Color>();
    }

    /// <summary>
    /// Settings for the keywords and colors.
    /// </summary>
    public class HightLightSettings
    {
        HightLightList m_rgKeywords = new HightLightList();
        Color default_color = new Color();
        bool enable= false;

        #region Properties
        /// <summary>
        /// A list containing all keywords.
        /// </summary>
        public List<string> Keywords
        {
            get { return m_rgKeywords.m_rgList; }
        }

        /// <summary>
        /// The color of keywords.
        /// </summary>
        public List<Color> KeywordColor
        {
            get { return m_rgKeywords.m_color; }
        }

        /// <summary>
        /// the color of other words
        /// </summary>
        public Color DefaultColor
        {
            get { return default_color; }
            set { default_color = value; }
        }

        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }
        #endregion
    }
}
