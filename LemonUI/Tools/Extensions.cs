using System.Drawing;

namespace LemonUI.Tools
{
    /// <summary>
    /// Extensions for converting values between relative and absolute.
    /// </summary>
    public static class Extensions
    {
        #region Float

        /// <summary>
        /// Converts an absolute X or Width float to a relative one.
        /// </summary>
        /// <param name="x">The float to convert.</param>
        /// <returns>A relative float between 0 and 1.</returns>
        public static float ToXRelative(this float x) => x / (1080f * GameScreen.AspectRatio);
        /// <summary>
        /// Converts an absolute Y or Height float to a relative one.
        /// </summary>
        /// <param name="y">The float to convert.</param>
        /// <returns>A relative float between 0 and 1.</returns>
        public static float ToYRelative(this float y) => y / 1080f;
        /// <summary>
        /// Converts an relative X or Width float to an absolute one.
        /// </summary>
        /// <param name="x">The float to convert.</param>
        /// <returns>An absolute float.</returns>
        public static float ToXAbsolute(this float x) => (1080f * GameScreen.AspectRatio) * x;
        /// <summary>
        /// Converts an relative Y or Height float to an absolute one.
        /// </summary>
        /// <param name="y">The float to convert.</param>
        /// <returns>An absolute float.</returns>
        public static float ToYAbsolute(this float y) => 1080f * y;

        #endregion

        #region PointF

        /// <summary>
        /// Converts an absolute 1080-based position into a relative one.
        /// </summary>
        /// <param name="point">The absolute PointF.</param>
        /// <returns>A new PointF with relative values.</returns>
        public static PointF ToRelative(this PointF point) => new PointF(point.X.ToXRelative(), point.Y.ToYRelative());
        /// <summary>
        /// Converts a normalized 0-1 position into an absolute one.
        /// </summary>
        /// <param name="point">The relative PointF.</param>
        /// <returns>A new PointF with absolute values.</returns>
        public static PointF ToAbsolute(this PointF point) => new PointF(point.X.ToXAbsolute(), point.Y.ToYAbsolute());

        #endregion

        #region SizeF

        /// <summary>
        /// Converts an absolute 1080-based size into a relative one.
        /// </summary>
        /// <param name="size">The absolute SizeF.</param>
        /// <returns>A new SizeF with relative values.</returns>
        public static SizeF ToRelative(this SizeF size) => new SizeF(size.Width.ToXAbsolute(), size.Height.ToYAbsolute());
        /// <summary>
        /// Converts a normalized 0-1 size into an absolute one.
        /// </summary>
        /// <param name="size">The relative SizeF.</param>
        /// <returns>A new SizeF with absolute values.</returns>
        public static SizeF ToAbsolute(this SizeF size) => new SizeF(size.Width.ToXRelative(), size.Height.ToYRelative());

        #endregion
    }
}
