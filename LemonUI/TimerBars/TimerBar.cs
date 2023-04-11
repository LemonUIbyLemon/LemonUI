#if FIVEM
using CitizenFX.Core.UI;
#elif RAGEMP
using RAGE.Game;
#elif SHVDN3
using GTA.UI;
#endif
using LemonUI.Elements;
using LemonUI.Extensions;
using System;
using System.Drawing;

namespace LemonUI.TimerBars
{
    /// <summary>
    /// Represents a Bar with text information shown in the bottom right.
    /// </summary>
    public class TimerBar : IDrawable
    {
        #region Fields

        /// <summary>
        /// The separation between the different timer bars.
        /// </summary>
        internal const float separation = 6.25f;
        /// <summary>
        /// The width of the background.
        /// </summary>
        internal static float backgroundWidth = 250;
        /// <summary>
        /// The height of the background.
        /// </summary>
        internal const float backgroundHeight = 37;
        /// <summary>
        /// The right edge padding for timer bar's info text.
        /// </summary>
        internal const float paddingRightText = 5;
        /// <summary>
        /// The right edge padding for timer bar's non-text info elements, such as <see cref="TimerBarObjective"/> or <see cref="TimerBarProgress"/>.
        /// </summary>
        internal const float paddingRightNonText = 10;
        /// <summary>
        /// The left edge padding for timer bar's title.
        /// </summary>
        internal const float paddingLeft = 90;

        /// <summary>
        /// The background of the timer bar.
        /// </summary>
        protected internal readonly ScaledTexture background = new ScaledTexture("timerbars", "all_black_bg")
        {
            Color = Color.FromArgb(160, 255, 255, 255)
        };
        /// <summary>
        /// The title of the timer bar.
        /// </summary>
        protected internal readonly ScaledText title = new ScaledText(PointF.Empty, string.Empty, 0.29f)
        {
            Alignment = Alignment.Right,
            WordWrap = 1000
        };
        /// <summary>
        /// The information of the Timer Bar.
        /// </summary>
        protected internal readonly ScaledText info = new ScaledText(PointF.Empty, string.Empty, 0.5f)
        {
            Alignment = Alignment.Right,
            WordWrap = 1000
        };

        private string rawTitle = string.Empty;
        private string rawInfo = string.Empty;

        #endregion

        #region Properties

        /// <summary>
        /// The title of the bar, shown on the left.
        /// </summary>
        public string Title
        {
            get => rawTitle;
            set
            {
                rawTitle = value;
                title.Text = value.ToUpperInvariant();
            }
        }
        /// <summary>
        /// The information shown on the right.
        /// </summary>
        public string Info
        {
            get => rawInfo;
            set
            {
                rawInfo = value;
                info.Text = value.ToUpperInvariant();
            }
        }
        /// <summary>
        /// The Width of the information text.
        /// </summary>
        public float InfoWidth => info.Width;
        /// <summary>
        /// The color of the information text.
        /// </summary>
        public Color Color
        {
            get => info.Color;
            set => info.Color = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="TimerBar"/> with the specified Title and Value.
        /// </summary>
        /// <param name="title">The title of the bar.</param>
        /// <param name="info">The information shown on the bar.</param>
        public TimerBar(string title, string info)
        {
            Title = title;
            Info = info;
        }

        #endregion

        #region Functions

        /// <summary>
        /// Recalculates the position of the timer bar elements based on the location of it on the screen.
        /// </summary>
        /// <param name="pos">The Top Left position of the Timer Bar.</param>
        public virtual void Recalculate(PointF pos)
        {
            float titleX = pos.X + paddingLeft;
            float titleY = pos.Y + ((backgroundHeight / 2) - (title.LineHeight / 2));
            float infoX = pos.X + backgroundWidth - paddingRightText;
            float infoY = pos.Y - 3;
            background.Position = pos;
            background.Size = new SizeF(backgroundWidth, backgroundHeight);
            title.Position = new PointF(titleX, titleY);
            info.Position = new PointF(infoX, infoY);
        }
        /// <summary>
        /// Draws the timer bar information.
        /// </summary>
        public virtual void Draw()
        {
            background.Draw();
            title.Draw();
            info.Draw();
        }

        #endregion
    }
}
