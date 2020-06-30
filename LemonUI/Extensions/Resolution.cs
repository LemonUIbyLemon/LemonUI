#if FIVEM
using CitizenFX.Core.Native;
#else
using GTA.Native;
#endif

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
    }
}
