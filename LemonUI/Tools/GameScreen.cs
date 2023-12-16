#if FIVEM
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
#elif RAGEMP
using RAGE.Game;
#elif RPH
using Rage;
using Rage.Native;
using Control = Rage.GameControl;
#elif SHVDN3 || SHVDNC
using GTA;
using GTA.Native;
#elif ALTV
using AltV.Net.Client;
using LemonUI.Elements;
#endif
using System.Drawing;

namespace LemonUI.Tools
{
    /// <summary>
    /// The screen of the game being rendered.
    /// </summary>
    public static class GameScreen
    {
        #region Properties

        /// <summary>
        /// The Aspect Ratio of the screen.
        /// </summary>
        public static float AspectRatio
        {
            get
            {
#if FIVEM
                return API.GetAspectRatio(false);
#elif RAGEMP
                return Invoker.Invoke<float>(Natives.GetAspectRatio);
#elif RPH
                return NativeFunction.CallByHash<float>(0xF1307EF624A80D87, false);
#elif SHVDN3 || SHVDNC
                return Function.Call<float>(Hash.GET_ASPECT_RATIO, false);
#elif ALTV
                return Alt.Natives.GetAspectRatio(false);
#endif
            }
        }
        /// <summary>
        /// The location of the cursor on screen between 0 and 1.
        /// </summary>
        public static PointF Cursor
        {
            get
            {
#if FIVEM
                float cursorX = API.GetControlNormal(0, (int)Control.CursorX);
                float cursorY = API.GetControlNormal(0, (int)Control.CursorY);
#elif ALTV
                float cursorX = Alt.Natives.GetControlNormal(0, (int)Control.CursorX);
                float cursorY = Alt.Natives.GetControlNormal(0, (int)Control.CursorY);
#elif RAGEMP
                float cursorX = Invoker.Invoke<float>(Natives.GetControlNormal, 0, (int)Control.CursorX);
                float cursorY = Invoker.Invoke<float>(Natives.GetControlNormal, 0, (int)Control.CursorY);
#elif RPH
                float cursorX = NativeFunction.CallByHash<float>(0xEC3C9B8D5327B563, 0, (int)Control.CursorX);
                float cursorY = NativeFunction.CallByHash<float>(0xEC3C9B8D5327B563, 0, (int)Control.CursorY);
#elif SHVDN3 || SHVDNC
                float cursorX = Function.Call<float>(Hash.GET_CONTROL_NORMAL, 0, (int)Control.CursorX);
                float cursorY = Function.Call<float>(Hash.GET_CONTROL_NORMAL, 0, (int)Control.CursorY);
#endif
                return new PointF(cursorX.ToXAbsolute(), cursorY.ToYAbsolute());
            }
        }

        #endregion

        #region Functions

        /// <summary>
        /// Checks if the cursor is inside of the absolute area.
        /// </summary>
        /// <param name="pos">The absolute position.</param>
        /// <param name="size">The absolute size of the area.</param>
        /// <returns><see langword="true"/> if the cursor is in the specified bounds, <see langword="false"/> otherwise.</returns>
        public static bool IsCursorInArea(PointF pos, SizeF size) => IsCursorInArea(pos.X, pos.Y, size.Width, size.Height);
        /// <summary>
        /// Checks if the cursor is inside of the absolute area.
        /// </summary>
        /// <param name="x">The absolute X position.</param>
        /// <param name="y">The absolute Y position.</param>
        /// <param name="width">The absolute width of the area.</param>
        /// <param name="height">The absolute height of the area.</param>
        /// <returns><see langword="true"/> if the cursor is in the specified bounds, <see langword="false"/> otherwise.</returns>
        public static bool IsCursorInArea(float x, float y, float width, float height)
        {
            PointF cursorPosition = Cursor;

            bool isX = cursorPosition.X >= x && cursorPosition.X <= x + width;
            bool isY = cursorPosition.Y > y && cursorPosition.Y < y + height;
            return isX && isY;
        }
        /// <summary>
        /// Shows the cursor during the current game frame.
        /// </summary>
        public static void ShowCursorThisFrame()
        {
#if FIVEM
            API.SetMouseCursorActiveThisFrame();
#elif ALTV
            Alt.Natives.SetMouseCursorThisFrame();
#elif RAGEMP
            Invoker.Invoke(0xAAE7CE1D63167423);
#elif RPH
            NativeFunction.CallByHash<int>(0xAAE7CE1D63167423);
#elif SHVDN3 || SHVDNC
            Function.Call(Hash.SET_MOUSE_CURSOR_THIS_FRAME);
#endif
        }

        #endregion
    }
}
