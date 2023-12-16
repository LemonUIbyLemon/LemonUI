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
        /// Converts an absolute 1080-based position into a relative one.
        /// </summary>
        /// <param name="point">The absolute PointF.</param>
        /// <returns>A new PointF with relative values.</returns>
        public static PointF ToRelative(this PointF point) => Tools.Extensions.ToRelative(point);
        /// <summary>
        /// Converts a normalized 0-1 position into an absolute one.
        /// </summary>
        /// <param name="point">The relative PointF.</param>
        /// <returns>A new PointF with absolute values.</returns>
        public static PointF ToAbsolute(this PointF point) => Tools.Extensions.ToAbsolute(point);
    }
}
