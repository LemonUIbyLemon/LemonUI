#if ALTV
using AltV.Net.Client;
#elif FIVEM
using CitizenFX.Core;
#elif RAGEMP
using RAGE.Game;
#elif RPH
using Rage.Native;
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
    public class ScaledAnim : BaseElement
    {
        #region Fields

        private readonly ScaledTexture texture = new ScaledTexture(string.Empty, string.Empty);
        private float frameRate;
        private int start = 0;
        private int duration;

        #endregion

        #region Properties

        /// <summary>
        /// The position of this animation.
        /// </summary>
        public override PointF Position
        {
            get => texture.Position;
            set => texture.Position = value;
        }
        /// <summary>
        /// The size of this animation.
        /// </summary>
        public override SizeF Size
        {
            get => texture.Size;
            set => texture.Size = value;
        }
        /// <summary>
        /// The color of this animation.
        /// </summary>
        public override Color Color
        {
            get => texture.Color;
            set => texture.Color = value;
        }
        /// <summary>
        /// The rotation of this animation.
        /// </summary>
        public override float Heading
        {
            get => texture.Heading;
            set => texture.Heading = value;
        }
        /// <summary>
        /// The dictionary that contains the textures.
        /// </summary>
        public string Dictionary
        {
            get => texture.Dictionary;
            set => texture.Dictionary = value ?? throw new ArgumentNullException(nameof(value));
        }
        /// <summary>
        /// The total number of frames per second.
        /// </summary>
        public float FrameRate
        {
            get => frameRate;
            set
            {
                if (frameRate <= 0)
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
        /// <param name="pos">The position of the animation.</param>
        /// <param name="size">The size of the animation.</param>
        public ScaledAnim(string dict, PointF pos, SizeF size) : base(pos, size)
        {
            texture.Dictionary = dict ?? throw new ArgumentNullException(nameof(dict));
        }

        #endregion

        #region Functions

        /// <summary>
        /// Draws the animation.
        /// </summary>
        public override void Draw()
        {
            if (Duration <= 0)
            {
                return;
            }

#if ALTV
            int time = Alt.Natives.GetGameTimer();
#elif RAGEMP
            int time = Misc.GetGameTimer();
#elif RPH
            int time = NativeFunction.CallByHash<int>(0x9CD27B0045628463);
#elif FIVEM || SHVDN3 || SHVDNC
            int time = Game.GameTime;
#endif

            int end = start + Duration;

            if (start == 0 || end <= time)
            {
                start = time;
            }

            float progress = (time - (float)start) / Duration;
            int totalFrames = (int)((duration / 1000.0f) * frameRate);
            int currentFrame = (int)(totalFrames * progress);
            texture.Texture = currentFrame.ToString();

            texture.Draw();
        }

        #endregion
    }
}
