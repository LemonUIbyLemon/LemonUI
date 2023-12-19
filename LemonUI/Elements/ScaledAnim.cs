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

        #endregion

        #region Properties

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
            set => frameRate = value;
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
        }

        #endregion
    }
}
