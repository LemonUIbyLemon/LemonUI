#if FIVEM
using CitizenFX.Core.UI;
#elif SHVDN2
using GTA;
#elif SHVDN3
using GTA.UI;
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
        /// Converts a 1080-based resolution into a relative one.
        /// </summary>
        /// <param name="xin">The absolute X value.</param>
        /// <param name="yin">The absolute Y value.</param>
        /// <param name="xout">The relative X value.</param>
        /// <param name="yout">The absolute Y value.</param>
        public static void ToRelative(float xin, float yin, out float xout, out float yout)
        {

            // Get the resolution of the game window
#if SHVDN2
            SizeF screenSize = Game.ScreenResolution;
#else
            SizeF screenSize = Screen.Resolution;
#endif
            // Calculate the ratio of the resolution (height relative to the width)
            float ratio = screenSize.Width / screenSize.Height;
            // And get the real width
            float width = 1080f * ratio;

            // And save the correct position and sizes
            xout = xin / width;
            yout = yin / 1080f;
        }
    }
}
