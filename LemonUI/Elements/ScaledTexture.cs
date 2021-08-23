#if FIVEM
using CitizenFX.Core.Native;
#elif RPH
using Rage.Native;
#elif (SHVDN2 || SHVDN3)
using GTA.Native;
#endif
using System.Drawing;

namespace LemonUI.Elements
{
    /// <summary>
    /// A 2D game texture.
    /// </summary>
    public class ScaledTexture : BaseElement
    {
        #region Public Properties

        /// <summary>
        /// The dictionary where the texture is loaded.
        /// </summary>
        public string Dictionary { get; set; }
        /// <summary>
        /// The texture to draw from the dictionary.
        /// </summary>
        public string Texture { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="ScaledTexture"/> with a Position and Size of Zero.
        /// </summary>
        /// <param name="dictionary">The dictionary where the texture is located.</param>
        /// <param name="texture">The texture to draw.</param>
        public ScaledTexture(string dictionary, string texture) : this(PointF.Empty, SizeF.Empty, dictionary, texture)
        {
        }
        /// <summary>
        /// Creates a new <see cref="ScaledTexture"/> with a Position and Size of zero.
        /// </summary>
        /// <param name="pos">The position of the Texture.</param>
        /// <param name="size">The size of the Texture.</param>
        /// <param name="dictionary">The dictionary where the texture is located.</param>
        /// <param name="texture">The texture to draw.</param>
        public ScaledTexture(PointF pos, SizeF size, string dictionary, string texture) : base(pos, size)
        {
            Dictionary = dictionary;
            Texture = texture;
            Request();
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// Requests the texture dictionary for this class.
        /// </summary>
        private void Request()
        {
#if FIVEM
            if (!API.HasStreamedTextureDictLoaded(Dictionary))
            {
                API.RequestStreamedTextureDict(Dictionary, true);
            }
#elif RPH
            if (!NativeFunction.CallByHash<bool>(0x0145F696AAAAD2E4, Dictionary))
            {
                NativeFunction.CallByHash<int>(0xDFA2EF8E04127DD5, Dictionary, true);
            }
#elif (SHVDN2 || SHVDN3)
            if (!Function.Call<bool>(Hash.HAS_STREAMED_TEXTURE_DICT_LOADED, Dictionary))
            {
                Function.Call(Hash.REQUEST_STREAMED_TEXTURE_DICT, Dictionary, true);
            }
#endif
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Draws the texture on the screen.
        /// </summary>
        public override void Draw()
        {
            if (Size == SizeF.Empty)
            {
                return;
            }
            Request();
#if FIVEM
            API.DrawSprite(Dictionary, Texture, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, Heading, Color.R, Color.G, Color.B, Color.A);
#elif RPH
            NativeFunction.CallByHash<int>(0xE7FFAE5EBF23D890, Dictionary, Texture, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, Heading, Color.R, Color.G, Color.B, Color.A);
#elif (SHVDN2 || SHVDN3)
            Function.Call(Hash.DRAW_SPRITE, Dictionary, Texture, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, Heading, Color.R, Color.G, Color.B, Color.A);
#endif
        }
        /// <summary>
        /// Draws a specific part of the texture on the screen.
        /// </summary>
        public void DrawSpecific(PointF topLeft, PointF bottomRight)
        {
            if (Size == SizeF.Empty)
            {
                return;
            }
            Request();
#if FIVEM
            API.DrawSpriteUv(Dictionary, Texture, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, topLeft.X, topLeft.Y, bottomRight.X, bottomRight.Y, Heading, Color.R, Color.G, Color.B, Color.A);
#elif RPH
            NativeFunction.CallByHash<int>(0x95812F9B26074726, Dictionary, Texture, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, topLeft.X, topLeft.Y, bottomRight.X, bottomRight.Y, Heading, Color.R, Color.G, Color.B, Color.A);
#elif (SHVDN2 || SHVDN3)
            Function.Call((Hash)0x95812F9B26074726, Dictionary, Texture, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, topLeft.X, topLeft.Y, bottomRight.X, bottomRight.Y, Heading, Color.R, Color.G, Color.B, Color.A);
#endif
        }
        /// <summary>
        /// Recalculates the position based on the size.
        /// </summary>
        public override void Recalculate()
        {
            base.Recalculate();
            relativePosition.X += relativeSize.Width * 0.5f;
            relativePosition.Y += relativeSize.Height * 0.5f;
        }

        #endregion
    }
}
