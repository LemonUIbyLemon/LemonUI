#if FIVEM
using CitizenFX.Core;
using CitizenFX.Core.Native;
#else
using GTA;
using GTA.Native;
#endif
using System.Drawing;

namespace LemonUI.Extensions
{
    /// <summary>
    /// Tools for dealing with resolutions.
    /// </summary>
    internal static class Resolution
    {
        /// <summary>
        /// Converts a normalized 0-1 resolution into an absolute one.
        /// </summary>
        /// <param name="xin">The original relative X value.</param>
        /// <param name="yin">The calculated absolute Y value.</param>
        /// <param name="xout">The original relative X value.</param>
        /// <param name="yout">The calculated absolute Y value.</param>
        public static void ToAbsolute(float xin, float yin, out float xout, out float yout)
        {
            // Get the ratio of the resolution
#if FIVEM
            float ratio = API.GetAspectRatio(false);
#elif SHVDN2
            float ratio = Function.Call<float>(Hash._0xF1307EF624A80D87, false);
#elif SHVDN3
            float ratio = Function.Call<float>(Hash._GET_ASPECT_RATIO, false);
#endif
            // Get the real width
            float width = 1080f * ratio;
            // And save the correct values
            xout = width * xin;
            yout = 1080f * yin;
        }
        /// <summary>
        /// Converts a 1080-based resolution into a relative one.
        /// </summary>
        /// <param name="xin">The absolute X value.</param>
        /// <param name="yin">The absolute Y value.</param>
        /// <param name="xout">The relative X value.</param>
        /// <param name="yout">The absolute Y value.</param>
        public static void ToRelative(float xin, float yin, out float xout, out float yout)
        {
            // Get the ratio of the resolution
#if FIVEM
            float ratio = API.GetAspectRatio(false);
#elif SHVDN2
            float ratio = Function.Call<float>(Hash._0xF1307EF624A80D87, false);
#elif SHVDN3
            float ratio = Function.Call<float>(Hash._GET_ASPECT_RATIO, false);
#endif
            // Get the real width
            float width = 1080f * ratio;
            // And save the correct values
            xout = xin / width;
            yout = yin / 1080f;
        }
        /// <summary>
        /// Checks if the cursor is at the specified location.
        /// </summary>
        /// <param name="pos">The position of the area.</param>
        /// <param name="size">The size of the area to check.</param>
        /// <returns></returns>
        public static bool IsCursorInBounds(PointF pos, SizeF size) => IsCursorInBounds(pos.X, pos.Y, size.Width, size.Height);
        /// <summary>
        /// Checks if the cursor is at the specified location.
        /// </summary>
        /// <param name="x">The absolute X position.</param>
        /// <param name="y">The absolute Y position.</param>
        /// <param name="width">The absolute Width from X.</param>
        /// <param name="height">The absolute Height from Y.</param>
        /// <returns>True if the mouse is in the specified location, False otherwise.</returns>
        public static bool IsCursorInBounds(float x, float y, float width, float height)
        {
            // Get the location where the cursor is
#if FIVEM
            float cursorX = API.GetControlNormal(0, (int)Control.CursorX);
            float cursorY = API.GetControlNormal(0, (int)Control.CursorY);
#else
            float cursorX = Function.Call<float>(Hash.GET_CONTROL_NORMAL, 0, (int)Control.CursorX);
            float cursorY = Function.Call<float>(Hash.GET_CONTROL_NORMAL, 0, (int)Control.CursorY);
#endif
            // Convert the values received to relative
            ToRelative(x, y, out float startX, out float startY);
            ToRelative(width, height, out float realWidth, out float realHeight);
            // And get the correct positions of them
            float realX = 0, realY = 0;
#if FIVEM
            API.GetScriptGfxPosition(startX, startY, ref realX, ref realY);
#else
            OutputArgument argX = new OutputArgument();
            OutputArgument argY = new OutputArgument();
            Function.Call((Hash)0x6DD8F5AA635EB4B2, startX, startY, argX, argY);
            realX = argX.GetResult<float>();
            realY = argY.GetResult<float>();
#endif
            // Check if the values are in the correct positions
            bool isX = cursorX >= realX && cursorX <= realX + realWidth;
            bool isY = cursorY > realY && cursorY < realY + realHeight;
            // And return the result of the checks
            return isX && isY;
        }
    }
}
