using System.ComponentModel;

namespace EzPPPwn.Controls
{
    public enum ProgressBarDisplayMode
    {
        NoText,
        TextAndPercentage,
        TextAndCurrProgress
    }

    public class DP_TextProgressBar : ProgressBar
    {
        public DP_TextProgressBar()
        {
            Value = Minimum;
            FixComponentBlinking();
        }
        public new void Dispose()
        {
            _textColourBrush.Dispose();
            _progressColourBrush.Dispose();
            base.Dispose();
        }
        #region Attributes
        private string CurrProgressStr
        {
            get
            {
                return $"{Value}/{Maximum}";
            }
        }

        private string _text = string.Empty;
        [Description("If it's empty, % will be shown"), Category("Additional Options"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string CustomText
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                Invalidate();//redraw component after change value from VS Properties section
            }
        }

        private string PercentageStr { get { return $"{Math.Round(((double)Value - Minimum) / ((double)Maximum - Minimum) * 100, Round)} %"; } }

        private SolidBrush _progressColourBrush = (SolidBrush)Brushes.LightGreen;
        [Category("Additional Options"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Color ProgressColor
        {
            get
            {
                return _progressColourBrush.Color;
            }
            set
            {
                _progressColourBrush.Dispose();
                _progressColourBrush = new SolidBrush(value);
            }
        }

        private int _round = 2;
        [Description("Round %"), Category("Additional Options"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public int Round
        {
            get
            {
                return _round;
            }
            set
            {
                _round = value;
                Invalidate();//redraw component after change value from VS Properties section
            }
        }

        private SolidBrush _textColourBrush = (SolidBrush)Brushes.Black;
        [Category("Additional Options")]
        public Color TextColor
        {
            get
            {
                return _textColourBrush.Color;
            }
            set
            {
                _textColourBrush.Dispose();
                _textColourBrush = new SolidBrush(value);
            }
        }

        [Description("Font of the text on ProgressBar"), Category("Additional Options")]
        public Font TextFont { get; set; } = new Font(FontFamily.GenericSerif, 11, FontStyle.Bold | FontStyle.Italic);

        private string TextToDraw
        {
            get
            {
                string text = CustomText;

                switch (VisualMode)
                {
                    case ProgressBarDisplayMode.TextAndCurrProgress:
                        text = $"{CustomText} {CurrProgressStr}";
                        break;
                    case ProgressBarDisplayMode.TextAndPercentage:
                        text = $"{CustomText} {PercentageStr}";
                        break;
                }

                return text;
            }
            set { }
        }

        private ProgressBarDisplayMode _visualMode = ProgressBarDisplayMode.TextAndPercentage;
        [Category("Additional Options"), Browsable(true)]
        public ProgressBarDisplayMode VisualMode
        {
            get
            {
                return _visualMode;
            }
            set
            {
                _visualMode = value;
                Invalidate();//redraw component after change value from VS Properties section
            }
        }
        #endregion
        #region EVENTS

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            DrawProgressBar(g);

            DrawStringIfNeeded(g);
        }
        #endregion
        #region FUNCTIONS
        private void DrawProgressBar(Graphics g)
        {
            Rectangle rect = ClientRectangle;

            ProgressBarRenderer.DrawHorizontalBar(g, rect);

            rect.Inflate(-3, -3);

            if (Value > 0)
            {
                Rectangle clip = new(rect.X, rect.Y, (int)Math.Round((double)Value / Maximum * rect.Width), rect.Height);

                g.FillRectangle(_progressColourBrush, clip);
            }
        }
        private void DrawStringIfNeeded(Graphics g)
        {
            if (VisualMode != ProgressBarDisplayMode.NoText)
            {

                string text = TextToDraw;

                SizeF len = g.MeasureString(text, TextFont);

                Point location = new(Width / 2 - (int)len.Width / 2, Height / 2 - (int)len.Height / 2);

                g.DrawString(text, TextFont, _textColourBrush, location);
            }
        }
        private void FixComponentBlinking()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }
        #endregion
    }
}

