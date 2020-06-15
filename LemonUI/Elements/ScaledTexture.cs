#if FIVEM
using CitizenFX.Core.Native;
#else
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

        public ScaledTexture(PointF pos, SizeF size, string dictionary, string texture) : base(pos, size)
        {
            // Save the dictionary and textures
            Dictionary = dictionary;
            Texture = texture;
            // And request the texture dictionary
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
#else
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
            Request();
#if FIVEM
            API.DrawSprite(Dictionary, Texture, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, Heading, Color.R, Color.G, Color.B, Color.A);
#else
            Function.Call(Hash.DRAW_SPRITE, Dictionary, Texture, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, Heading, Color.R, Color.G, Color.B, Color.A);
#endif
        }

        #endregion
    }
}
