#if FIVEM
using CitizenFX.Core;
using CitizenFX.Core.Native;
#else
using GTA;
using GTA.Native;
#endif
using System.Drawing;

namespace LemonUI
{
    /// <summary>
    /// Contains a set of tools to work with the screen information.
    /// </summary>
    public static class Screen
    {
        /// <summary>
        /// The Aspect Ratio of the screen resolution.
        /// </summary>
        public static float AspectRatio
        {
            get
            {
#if FIVEM
                return API.GetAspectRatio(false);
#elif SHVDN2
                return Function.Call<float>(Hash._0xF1307EF624A80D87, false);
#elif SHVDN3
                return Function.Call<float>(Hash._GET_ASPECT_RATIO, false);
#endif
            }
        }

        /// <summary>
        /// Converts a relative resolution into one scaled to 1080p.
        /// </summary>
        /// <param name="relativeX">The relative value of X.</param>
        /// <param name="relativeY">The relative value of Y.</param>
        /// <param name="absoluteX">The value of X scaled to 1080p.</param>
        /// <param name="absoluteY">The value of Y scaled to 1080p.</param>
        public static void ToAbsolute(float relativeX, float relativeY, out float absoluteX, out float absoluteY)
        {
            // Get the real width based on the aspect ratio
            float width = 1080f * AspectRatio;
            // And save the correct values
            absoluteX = width * relativeX;
            absoluteY = 1080f * relativeY;
        }
        /// <summary>
        /// Converts a 1080p-based resolution into relative values.
        /// </summary>
        /// <param name="absoluteX">The 1080p based X coord.</param>
        /// <param name="absoluteY">The 1080p based Y coord.</param>
        /// <param name="relativeX">The value of X converted to relative.</param>
        /// <param name="relativeY">The value of Y converted to relative.</param>
        public static void ToRelative(float absoluteX, float absoluteY, out float relativeX, out float relativeY)
        {
            // Get the real width based on the aspect ratio
            float width = 1080f * AspectRatio;
            // And save the correct values
            relativeX = absoluteX / width;
            relativeY = absoluteY / 1080f;
        }
        /// <summary>
        /// Checks if the cursor is inside of the specified area.
        /// </summary>
        /// <remarks>
        /// This function takes values scaled to 1080p and is aware of the alignment set via SET_SCRIPT_GFX_ALIGN.
        /// </remarks>
        /// <param name="pos">The start of the area.</param>
        /// <param name="size">The size of the area to check.</param>
        /// <returns><see langword="true"/> if the cursor is in the specified bounds, <see langword="false"/> otherwise.</returns>
        public static bool IsCursorInArea(PointF pos, SizeF size) => IsCursorInArea(pos.X, pos.Y, size.Width, size.Height);
        /// <summary>
        /// Checks if the cursor is inside of the specified area.
        /// </summary>
        /// <remarks>
        /// This function takes values scaled to 1080p and is aware of the alignment set via SET_SCRIPT_GFX_ALIGN.
        /// </remarks>
        /// <param name="x">The start X position.</param>
        /// <param name="y">The start Y position.</param>
        /// <param name="width">The height of the search area from X.</param>
        /// <param name="height">The height of the search area from Y.</param>
        /// <returns><see langword="true"/> if the cursor is in the specified bounds, <see langword="false"/> otherwise.</returns>
        public static bool IsCursorInArea(float x, float y, float width, float height)
        {
            // Get the current location of the cursor
#if FIVEM
            float cursorX = API.GetControlNormal(0, (int)Control.CursorX);
            float cursorY = API.GetControlNormal(0, (int)Control.CursorY);
#elif SHVDN2 || SHVDN3
            float cursorX = Function.Call<float>(Hash.GET_CONTROL_NORMAL, 0, (int)Control.CursorX);
            float cursorY = Function.Call<float>(Hash.GET_CONTROL_NORMAL, 0, (int)Control.CursorY);
#endif
            // Convert the search area values to relative
            ToRelative(x, y, out float startX, out float startY);
            ToRelative(width, height, out float realWidth, out float realHeight);
            // And get the correct on screen positions based on the GFX Alignment
            float realX = 0, realY = 0;
#if FIVEM
            API.GetScriptGfxPosition(startX, startY, ref realX, ref realY);
#else
            OutputArgument argX = new OutputArgument();
            OutputArgument argY = new OutputArgument();
            Function.Call((Hash)0x6DD8F5AA635EB4B2, startX, startY, argX, argY); // _GET_SCRIPT_GFX_POSITION
            realX = argX.GetResult<float>();
            realY = argY.GetResult<float>();
#endif
            // Check if the values are in the correct positions
            bool isX = cursorX >= realX && cursorX <= realX + realWidth;
            bool isY = cursorY > realY && cursorY < realY + realHeight;
            // And return the result of those checks
            return isX && isY;
        }
        /// <summary>
        /// Shows the cursor during the current game frame.
        /// </summary>
        public static void ShowCursorThisFrame()
        {
#if FIVEM
            API.SetMouseCursorActiveThisFrame();
#elif SHVDN2
            Function.Call(Hash._SHOW_CURSOR_THIS_FRAME);
#elif SHVDN3
            Function.Call(Hash._SET_MOUSE_CURSOR_ACTIVE_THIS_FRAME);
#endif
        }
    }
}
