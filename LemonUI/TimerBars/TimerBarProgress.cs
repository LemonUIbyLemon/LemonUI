using LemonUI.Elements;
using System;
using System.Drawing;

namespace LemonUI.TimerBars
{
    /// <summary>
    /// Represents a Timer Bar that shows the progress of something.
    /// </summary>
    public class TimerBarProgress : TimerBar
    {
        #region Constant Fields

        private const float barWidth = 108;
        private const float barHeight = 15;

        #endregion

        #region Private Fields

        private float progress = 100;

        #endregion

        #region Internal Fields

        /// <summary>
        /// The background of the Progress Bar.
        /// </summary>
        internal protected readonly ScaledRectangle barBackground = new ScaledRectangle(PointF.Empty, SizeF.Empty)
        {
            Color = Color.FromArgb(255, 139, 0, 0)
        };
        /// <summary>
        /// The foreground of the Progress Bar.
        /// </summary>
        internal protected readonly ScaledRectangle barForeground = new ScaledRectangle(PointF.Empty, SizeF.Empty)
        {
            Color = Color.FromArgb(255, 255, 0, 0)
        };

        #endregion

        #region Public Properties

        /// <summary>
        /// The progress of the bar.
        /// </summary>
        public float Progress
        {
            get => progress;
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                progress = value;
                barForeground.Size = new SizeF(barWidth * (value * 0.01f), barHeight);
            }
        }
        /// <summary>
        /// The Foreground color of the Progress bar.
        /// </summary>
        public Color ForegroundColor
        {
            get => barForeground.Color;
            set => barForeground.Color = value;
        }
        /// <summary>
        /// The Background color of the Progress bar.
        /// </summary>
        public Color BackgroundColor
        {
            get => barBackground.Color;
            set => barBackground.Color = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="TimerBarProgress"/> with the specified title.
        /// </summary>
        /// <param name="title">The title of the bar.</param>
        public TimerBarProgress(string title) : base(title, "")
        {
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Recalculates the position of the timer bar elements based on the location of it on the screen.
        /// </summary>
        /// <param name="pos">The Top Left position of the Timer Bar.</param>
        public override void Recalculate(PointF pos)
        {
            // Recalculate the base elements
            base.Recalculate(pos);
            // And set the size and position of the progress bar
            PointF barPos = new PointF(pos.X + 103, pos.Y + 12);
            barBackground.Position = barPos;
            barBackground.Size = new SizeF(barWidth, barHeight);
            barForeground.Position = barPos;
            barForeground.Size = new SizeF(barWidth * (progress * 0.01f), barHeight);
        }
        /// <summary>
        /// Draws the TimerBar.
        /// </summary>
        public override void Draw()
        {
            background.Draw();
            title.Draw();
            barBackground.Draw();
            barForeground.Draw();
        }

        #endregion
    }
}
