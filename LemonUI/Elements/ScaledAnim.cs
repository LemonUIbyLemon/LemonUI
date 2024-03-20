#if ALTV
using AltV.Net.Client;
#elif FIVEM
using CitizenFX.Core;
#elif RAGEMP
using RAGE.Game;
#elif RPH
using Rage;
#elif SHVDN3 || SHVDNC
using GTA;
#endif
using System;
using System.Drawing;

namespace LemonUI.Elements
{
    /// <summary>
    /// A scaled animation using YTD files with all of the frames.
    /// </summary>
    public class ScaledAnim : ScaledTexture
    {
        #region Fields

        private float frameRate;
        private long start = 0;
        private int duration;

        #endregion

        #region Properties

        /// <summary>
        /// The total number of frames per second.
        /// </summary>
        public float FrameRate
        {
            get => frameRate;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The Frame Rate can't be equal or lower to zero.");
                }

                frameRate = value;
            }
        }
        /// <summary>
        /// The duration of the animation in milliseconds.
        /// </summary>
        public int Duration
        {
            get => duration;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The duration can't be under zero.");
                }

                duration = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new dictionary based animation.
        /// </summary>
        /// <param name="dict">The texture dictionary (YTD) to use.</param>
        public ScaledAnim(string dict) : this(dict, PointF.Empty, SizeF.Empty)
        {
        }
        /// <summary>
        /// Creates a new dictionary based animation.
        /// </summary>
        /// <param name="dict">The texture dictionary (YTD) to use.</param>
        /// <param name="size">The size of the animation.</param>
        public ScaledAnim(string dict, SizeF size) : this(dict, PointF.Empty, size)
        {
        }
        /// <summary>
        /// Creates a new dictionary based animation.
        /// </summary>
        /// <param name="dict">The texture dictionary (YTD) to use.</param>
        /// <param name="pos">The position of the animation.</param>
        /// <param name="size">The size of the animation.</param>
        public ScaledAnim(string dict, PointF pos, SizeF size) : base(pos, size, dict, string.Empty)
        {
            Dictionary = dict ?? throw new ArgumentNullException(nameof(dict));
        }

        #endregion

        #region Tools

        private void UpdateTexture()
        {
            if (Duration <= 0)
            {
                return;
            }

#if ALTV
            long time = Alt.Natives.GetGameTimer();
#elif RAGEMP
            long time = Misc.GetGameTimer();
#elif FIVEM || RPH || SHVDN3 || SHVDNC
            long time = Game.GameTime;
#endif

            long end = start + Duration;

            if (start == 0 || end <= time)
            {
                start = time;
            }

            float progress = (time - (float)start) / Duration;
            int totalFrames = (int)((duration / 1000.0f) * frameRate);
            int currentFrame = (int)(totalFrames * progress) + 1;

            if (progress < 0)
            {
                currentFrame = 1;
                start = time;
            }
            else if (currentFrame >= totalFrames)
            {
                currentFrame = totalFrames;
                start = time;
            }

            Texture = currentFrame.ToString();
        }

        #endregion

        #region Functions

        /// <summary>
        /// Draws the animation.
        /// </summary>
        public override void Draw()
        {
            UpdateTexture();
            base.Draw();
        }

        #endregion
    }
}
