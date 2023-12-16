#if ALTV
using AltV.Net.Client;
#elif RAGEMP
using RAGE.Game;
#elif FIVEM
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
#elif RPH
using Rage.Native;
#elif SHVDN3 || SHVDNC
using GTA.Native;
using GTA.UI;
#endif
using System;
using System.Drawing;

namespace LemonUI.Tools
{
    /// <summary>
    /// Tools for changing, resetting and retrieving the Safe Zone of the game.
    /// </summary>
    public static class SafeZone
    {
        #region Properties

        /// <summary>
        /// The top left corner after the safe zone.
        /// </summary>
        public static PointF TopLeft => GetPositionAt(PointF.Empty, GFXAlignment.Left, GFXAlignment.Top);
        /// <summary>
        /// The top right corner after the safe zone.
        /// </summary>
        public static PointF TopRight => GetPositionAt(PointF.Empty, GFXAlignment.Right, GFXAlignment.Top);
        /// <summary>
        /// The bottom left corner after the safe zone.
        /// </summary>
        public static PointF BottomLeft => GetPositionAt(PointF.Empty, GFXAlignment.Left, GFXAlignment.Bottom);
        /// <summary>
        /// The bottom right corner after the safe zone.
        /// </summary>
        public static PointF BottomRight => GetPositionAt(PointF.Empty, GFXAlignment.Right, GFXAlignment.Bottom);

        #endregion

        #region Tools

        private static GFXAlignment AlignmentToGFXAlignment(Alignment alignment)
        {
            switch (alignment)
            {
                case Alignment.Left:
                    return GFXAlignment.Left;
                case Alignment.Right:
                    return GFXAlignment.Right;
                case Alignment.Center:
                    return GFXAlignment.Center;
                default:
                    throw new ArgumentException("Alignment is not one of the allowed values (Left, Right, Center).", nameof(alignment));
            }
        }

        #endregion

        #region Functions

        /// <summary>
        /// Converts the specified position into one that is aware of the safe zone.
        /// </summary>
        /// <param name="og">The original 1080p based position.</param>
        /// <returns>A new 1080p based position that is aware of the the Alignment.</returns>
        public static PointF GetSafePosition(PointF og) => GetSafePosition(og.X, og.Y);
        /// <summary>
        /// Converts the specified position into one that is aware of <see cref="SetAlignment(GFXAlignment, GFXAlignment)"/>.
        /// </summary>
        /// <param name="x">The 1080p based X position.</param>
        /// <param name="y">The 1080p based Y position.</param>
        /// <returns>A new 1080p based position that is aware of the the Alignment.</returns>
        public static PointF GetSafePosition(float x, float y)
        {
            float relativeX = x.ToXRelative();
            float relativeY = y.ToYRelative();

            float realX = 0, realY = 0;
#if FIVEM
            API.GetScriptGfxPosition(relativeX, relativeY, ref realX, ref realY);
#elif ALTV
            Alt.Natives.GetScriptGfxAlignPosition(relativeX, relativeY, ref realX, ref realY);
#elif RAGEMP
            FloatReference argX = new FloatReference();
            FloatReference argY = new FloatReference();
            Invoker.Invoke<int>(0x6DD8F5AA635EB4B2, relativeX, relativeY, argX, argY);
            realX = argX.Value;
            realY = argY.Value;
#elif RPH
            using (NativePointer argX = new NativePointer(4))
            using (NativePointer argY = new NativePointer(4))
            {
                NativeFunction.CallByHash<int>(0x6DD8F5AA635EB4B2, relativeX, relativeY, argX, argY);
                realX = argX.GetValue<float>();
                realY = argY.GetValue<float>();
            }
#elif SHVDN3 || SHVDNC
            using (OutputArgument argX = new OutputArgument())
            using (OutputArgument argY = new OutputArgument())
            {
                Function.Call((Hash)0x6DD8F5AA635EB4B2, relativeX, relativeY, argX, argY); // _GET_SCRIPT_GFX_POSITION
                realX = argX.GetResult<float>();
                realY = argY.GetResult<float>();
            }
#endif

            return new PointF(realX.ToXAbsolute(), realY.ToYAbsolute());
        }
        /// <summary>
        /// Sets the alignment for the safe zone.
        /// </summary>
        /// <param name="horizontal">The Horizontal alignment of the items.</param>
        /// <param name="vertical">The vertical alignment of the items.</param>
        public static void SetAlignment(Alignment horizontal, GFXAlignment vertical) => SetAlignment(AlignmentToGFXAlignment(horizontal), vertical);
        /// <summary>
        /// Sets the alignment for the safe zone.
        /// </summary>
        /// <param name="horizontal">The Horizontal alignment of the items.</param>
        /// <param name="vertical">The vertical alignment of the items.</param>
        public static void SetAlignment(GFXAlignment horizontal, GFXAlignment vertical)
        {
#if FIVEM
            API.SetScriptGfxAlign((int)horizontal, (int)vertical);
            API.SetScriptGfxAlignParams(0, 0, 0, 0);
#elif ALTV
            Alt.Natives.SetScriptGfxAlign((int)horizontal, (int)vertical);
            Alt.Natives.SetScriptGfxAlignParams(0, 0, 0, 0);
#elif RAGEMP
            Invoker.Invoke(0xB8A850F20A067EB6, (int)horizontal, (int)vertical);
            Invoker.Invoke(0xF5A2C681787E579D, 0, 0, 0, 0);
#elif RPH
            NativeFunction.CallByHash<int>(0xB8A850F20A067EB6, (int)horizontal, (int)vertical);
            NativeFunction.CallByHash<int>(0xF5A2C681787E579D, 0, 0, 0, 0);
#elif SHVDN3 || SHVDNC
            Function.Call(Hash.SET_SCRIPT_GFX_ALIGN, (int)horizontal, (int)vertical);
            Function.Call(Hash.SET_SCRIPT_GFX_ALIGN_PARAMS, 0, 0, 0, 0);
#endif
        }
        /// <summary>
        /// Resets the alignment of the safe zone.
        /// </summary>
        public static void ResetAlignment()
        {
#if FIVEM
            API.ResetScriptGfxAlign();
#elif ALTV
            Alt.Natives.ResetScriptGfxAlign();
#elif RAGEMP
            Invoker.Invoke(0xE3A3DB414A373DAB);
#elif RPH
            NativeFunction.CallByHash<int>(0xE3A3DB414A373DAB);
#elif SHVDN3 || SHVDNC
            Function.Call(Hash.RESET_SCRIPT_GFX_ALIGN);
#endif
        }
        /// <summary>
        /// Gets the specified position with the specified safe zone alignment.
        /// </summary>
        /// <param name="position">The position to get.</param>
        /// <param name="horizontal">The horizontal alignment.</param>
        /// <param name="vertical">The vertical alignment.</param>
        /// <returns>The absolute safe zone alignment.</returns>
        public static PointF GetPositionAt(PointF position, Alignment horizontal, GFXAlignment vertical) => GetPositionAt(position, AlignmentToGFXAlignment(horizontal), vertical);
        /// <summary>
        /// Gets the specified position with the specified safe zone alignment.
        /// </summary>
        /// <param name="position">The position to get.</param>
        /// <param name="horizontal">The horizontal alignment.</param>
        /// <param name="vertical">The vertical alignment.</param>
        /// <returns>The absolute safe zone alignment.</returns>
        public static PointF GetPositionAt(PointF position, GFXAlignment horizontal, GFXAlignment vertical)
        {
            SetAlignment(horizontal, vertical);
            PointF pos = GetSafePosition(position);
            ResetAlignment();
            return pos;
        }

        #endregion
    }
}
