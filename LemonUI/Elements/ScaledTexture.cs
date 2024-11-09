﻿#if FIVEMV2
using CitizenFX.FiveM.Native;
#elif FIVEM
using CitizenFX.Core.Native;
#elif RAGEMP
using RAGE.Game;
#elif RPH
using Rage.Native;
#elif SHVDN3 || SHVDNC
using GTA.Native;
#elif ALTV
using AltV.Net.Client;
#endif
using System;
using System.Drawing;

namespace LemonUI.Elements
{
    /// <summary>
    /// A 2D game texture.
    /// </summary>
    public class ScaledTexture : BaseElement
    {
        #region Fields

        private string dictionary;
        private string texture;

        #endregion

        #region Properties

        /// <summary>
        /// The dictionary where the texture is loaded.
        /// </summary>
        public string Dictionary
        {
            get => dictionary;
            set => dictionary = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// The texture to draw from the dictionary.
        /// </summary>
        public string Texture
        {
            get => texture;
            set => texture = value ?? throw new ArgumentNullException(nameof(value));
        }

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
            Dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
            Texture = texture ?? throw new ArgumentNullException(nameof(texture));
            Request();
        }

        #endregion

        #region Tools

        /// <summary>
        /// Requests the texture dictionary for this class.
        /// </summary>
        private void Request()
        {
#if FIVEMV2
            if (!Natives.HasStreamedTextureDictLoaded(Dictionary))
            {
                Natives.RequestStreamedTextureDict(Dictionary, true);
            }
#elif FIVEM
            if (!API.HasStreamedTextureDictLoaded(Dictionary))
            {
                API.RequestStreamedTextureDict(Dictionary, true);
            }
#elif RAGEMP
            if (!Invoker.Invoke<bool>(Natives.HasStreamedTextureDictLoaded, Dictionary))
            {
                Invoker.Invoke(Natives.RequestStreamedTextureDict, Dictionary, true);
            }
#elif ALTV
            if (!Alt.Natives.HasStreamedTextureDictLoaded(Dictionary))
            {
                Alt.Natives.RequestStreamedTextureDict(Dictionary, true);
            }
#elif RPH
            if (!NativeFunction.CallByHash<bool>(0x0145F696AAAAD2E4, Dictionary))
            {
                NativeFunction.CallByHash<int>(0xDFA2EF8E04127DD5, Dictionary, true);
            }
#elif SHVDN3 || SHVDNC
            if (!Function.Call<bool>(Hash.HAS_STREAMED_TEXTURE_DICT_LOADED, Dictionary))
            {
                Function.Call(Hash.REQUEST_STREAMED_TEXTURE_DICT, Dictionary, true);
            }
#endif
        }

        #endregion

        #region Functions

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
#if FIVEMV2
            Natives.DrawSprite(Dictionary, Texture, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, Heading, Color.R, Color.G, Color.B, Color.A);
#elif FIVEM
            API.DrawSprite(Dictionary, Texture, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, Heading, Color.R, Color.G, Color.B, Color.A);
#elif RAGEMP
            Invoker.Invoke(Natives.DrawSprite, Dictionary, Texture, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, Heading, Color.R, Color.G, Color.B, Color.A);
#elif RPH
            NativeFunction.CallByHash<int>(0xE7FFAE5EBF23D890, Dictionary, Texture, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, Heading, Color.R, Color.G, Color.B, Color.A);
#elif ALTV
            Alt.Natives.DrawSprite(Dictionary, Texture, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, Heading, Color.R, Color.G, Color.B, Color.A, false, 0);
#elif SHVDN3 || SHVDNC
            Function.Call(Hash.DRAW_SPRITE, Dictionary, Texture, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, Heading, Color.R, Color.G, Color.B, Color.A);
#endif
        }
        /// <summary>
        /// Draws a specific part of the texture on the screen.
        /// </summary>
        /// <param name="topLeft">The top left corner of the area to draw.</param>
        /// <param name="bottomRight">The bottom right corner of the area to draw.</param>
        public virtual void DrawSpecific(PointF topLeft, PointF bottomRight)
        {
            if (Size == SizeF.Empty)
            {
                return;
            }
            Request();
#if FIVEMV2
            Natives.DrawSpriteUv(Dictionary, Texture, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, topLeft.X, topLeft.Y, bottomRight.X, bottomRight.Y, Heading, Color.R, Color.G, Color.B, Color.A);
#elif FIVEM
            API.DrawSpriteUv(Dictionary, Texture, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, topLeft.X, topLeft.Y, bottomRight.X, bottomRight.Y, Heading, Color.R, Color.G, Color.B, Color.A);
#elif RAGEMP
            Invoker.Invoke(0x95812F9B26074726, Dictionary, Texture, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, topLeft.X, topLeft.Y, bottomRight.X, bottomRight.Y, Heading, Color.R, Color.G, Color.B, Color.A);
#elif RPH
            NativeFunction.CallByHash<int>(0x95812F9B26074726, Dictionary, Texture, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, topLeft.X, topLeft.Y, bottomRight.X, bottomRight.Y, Heading, Color.R, Color.G, Color.B, Color.A);
#elif ALTV
            Alt.Natives.DrawSpriteArxWithUv(Dictionary, Texture, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, topLeft.X, topLeft.Y, bottomRight.X, bottomRight.Y, Heading, Color.R, Color.G, Color.B, Color.A, 0);
#elif SHVDN3 || SHVDNC
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
