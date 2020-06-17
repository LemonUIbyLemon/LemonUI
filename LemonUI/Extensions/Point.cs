using System.Drawing;

namespace LemonUI.Extensions
{
    /// <summary>
    /// Extensions for the Point and PointF classes.
    /// </summary>
    public static class PointExtensions
    {
        /// <summary>
        /// Converts an absolute 1080-based position into a relative one.
        /// </summary>
        /// <param name="point">The absolute PointF.</param>
        /// <returns>A new PointF with relative values.</returns>
        public static PointF ToRelative(this PointF point)
        {
            Resolution.ToRelative(point.X, point.Y, out float x, out float y);
            return new PointF(x, y);
        }
    }
}
