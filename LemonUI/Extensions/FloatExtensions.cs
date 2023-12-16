using System;

namespace LemonUI.Extensions
{
    /// <summary>
    /// Extensions for the float class.
    /// </summary>
    [Obsolete("Please use LemonUI.Tools.Extensions instead.", true)]
    public static class FloatExtensions
    {
        /// <summary>
        /// Converts an absolute X or Width float to a relative one.
        /// </summary>
        /// <param name="fin">The float to convert.</param>
        /// <returns>A relative float between 0 and 1.</returns>
        public static float ToXRelative(this float fin) => Tools.Extensions.ToXRelative(fin);
        /// <summary>
        /// Converts an absolute Y or Height float to a relative one.
        /// </summary>
        /// <param name="fin">The float to convert.</param>
        /// <returns>A relative float between 0 and 1.</returns>
        public static float ToYRelative(this float fin) => Tools.Extensions.ToYRelative(fin);
        /// <summary>
        /// Converts an relative X or Width float to an absolute one.
        /// </summary>
        /// <param name="fin">The float to convert.</param>
        /// <returns>An absolute float.</returns>
        public static float ToXAbsolute(this float fin) => Tools.Extensions.ToXAbsolute(fin);
        /// <summary>
        /// Converts an relative Y or Height float to an absolute one.
        /// </summary>
        /// <param name="fin">The float to convert.</param>
        /// <returns>An absolute float.</returns>
        public static float ToYAbsolute(this float fin) => Tools.Extensions.ToYAbsolute(fin);
    }
}
