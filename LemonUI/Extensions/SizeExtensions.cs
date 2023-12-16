using System;
using System.Drawing;

namespace LemonUI.Extensions
{
    /// <summary>
    /// Extensions for the Size and SizeF classes.
    /// </summary>
    [Obsolete("Please use LemonUI.Tools.Extensions instead.", true)]
    public static class SizeExtensions
    {
        /// <summary>
        /// Converts a scaled 1080-based size into a relative one.
        /// </summary>
        /// <param name="size">The scaled SizeF.</param>
        /// <returns>A new SizeF with relative values.</returns>
        public static SizeF ToRelative(this SizeF size) => Tools.Extensions.ToRelative(size);
        /// <summary>
        /// Converts a normalized 0-1 size into a scaled one.
        /// </summary>
        /// <param name="size">The relative SizeF.</param>
        /// <returns>A new SizeF with scaled values.</returns>
        public static SizeF ToAbsolute(this SizeF size) => Tools.Extensions.ToScaled(size);
    }
}
