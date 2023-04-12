using LemonUI.Elements;
using System;
using System.Collections.Generic;
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
        /// <summary>
        /// The divider lines of the Progress Bar.
        /// </summary>
        protected internal List<ScaledRectangle> barLines { get; } = new List<ScaledRectangle>()
        {
            new ScaledRectangle(PointF.Empty, SizeF.Empty),
            new ScaledRectangle(PointF.Empty, SizeF.Empty),
            new ScaledRectangle(PointF.Empty, SizeF.Empty),
            new ScaledRectangle(PointF.Empty, SizeF.Empty)
        };

        private float progress = 100;
        private bool showDividerLines = false;

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
        /// <summary>
        /// Enable or disable divider lines at the Progress bar.
        /// </summary>
        public bool ShowDividerLines
        {
            get => showDividerLines;
            set
            {
                showDividerLines = value;
                Recalculate(lastPosition);
            }
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
            lastPosition = pos;
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

            if (showDividerLines)
            {
                for (int i = 0; i < barLines.Count; i++)
                {
                    ScaledRectangle line = barLines[i];
                    float partWidth = barWidth / 5;
                    float halfLineWidth = line.Size.Width / 2;
                    float lineWidth = 2.85f;
                    float lineHeight = barHeight + 4;
                    float heightDelta = lineHeight - barHeight;
                    line.Size = new SizeF(lineWidth, lineHeight);
                    line.Position = new PointF(barX + partWidth + (i * partWidth) - halfLineWidth, barY - (heightDelta / 2));
                    line.Color = Color.Black;
                }
            }
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

            if (showTeamBadge)
            {
                teamBadge.Draw();
            }

            if (showDividerLines)
            {
                foreach (var bar in barLines)
                {
                    bar.Draw();
                }
            }
        }

        #endregion
    }
}
