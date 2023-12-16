using System;
using System.Drawing;

namespace LemonUI.Extensions
{
    /// <summary>
    /// Extensions for the Point and PointF classes.
    /// </summary>
    [Obsolete("Please use LemonUI.Tools.Extensions instead.", true)]
    public static class PointExtensions
    {
        /// <summary>
        /// Converts a scaled 1080-based position into a relative one.
        /// </summary>
        /// <param name="point">The scaled PointF.</param>
        /// <returns>A new PointF with relative values.</returns>
        public static PointF ToRelative(this PointF point) => Tools.Extensions.ToRelative(point);
        /// <summary>
        /// Converts a normalized 0-1 position into a scaled one.
        /// </summary>
        /// <param name="point">The relative PointF.</param>
        /// <returns>A new PointF with scaled values.</returns>
        public static PointF ToAbsolute(this PointF point) => Tools.Extensions.ToScaled(point);
    }
}
