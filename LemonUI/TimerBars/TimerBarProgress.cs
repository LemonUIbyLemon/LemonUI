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
        #region Constants

        private const float barWidth = 130;
        private const float barHeight = 10;

        #endregion

        #region Fields

        /// <summary>
        /// The background of the Progress Bar.
        /// </summary>
        protected internal readonly ScaledRectangle barBackground = new ScaledRectangle(PointF.Empty, SizeF.Empty)
        {
            Color = Color.FromArgb(255, 139, 0, 0)
        };
        /// <summary>
        /// The foreground of the Progress Bar.
        /// </summary>
        protected internal readonly ScaledRectangle barForeground = new ScaledRectangle(PointF.Empty, SizeF.Empty)
        {
            Color = Color.FromArgb(255, 255, 0, 0)
        };

        private float progress = 100;

        #endregion

        #region Properties

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
        public TimerBarProgress(string title) : base(title, string.Empty)
        {
        }

        #endregion

        #region Functions

        /// <summary>
        /// Recalculates the position of the timer bar elements based on the location of it on the screen.
        /// </summary>
        /// <param name="pos">The Top Left position of the Timer Bar.</param>
        public override void Recalculate(PointF pos)
        {
            // Recalculate the base elements
            base.Recalculate(pos);
            // Calculate X and Y to position the progress bar respecting background's right edge padding and centered vertically
            float barX = pos.X + backgroundWidth - barWidth - paddingRightNonText;
            float barY = pos.Y + ((backgroundHeight / 2) - (barHeight / 2));
            // And set the size and position of the progress bar
            PointF barPos = new PointF(barX, barY);
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
