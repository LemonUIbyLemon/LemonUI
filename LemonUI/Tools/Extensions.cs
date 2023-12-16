using System.Drawing;

namespace LemonUI.Tools
{
    /// <summary>
    /// Extensions for converting values between relative and scaled.
    /// </summary>
    public static class Extensions
    {
        #region Float

        /// <summary>
        /// Converts the scaled X or Width to a relative one.
        /// </summary>
        /// <param name="x">The value to convert.</param>
        /// <returns>A relative float between 0 and 1.</returns>
        public static float ToXRelative(this float x) => x / (1080f * GameScreen.AspectRatio);
        /// <summary>
        /// Converts the scaled Y or Height to a relative one.
        /// </summary>
        /// <param name="y">The value to convert.</param>
        /// <returns>A relative float between 0 and 1.</returns>
        public static float ToYRelative(this float y) => y / 1080f;
        /// <summary>
        /// Converts the relative X or Width float to a scaled one.
        /// </summary>
        /// <param name="x">The float to convert.</param>
        /// <returns>A scaled float.</returns>
        public static float ToXScaled(this float x) => (1080f * GameScreen.AspectRatio) * x;
        /// <summary>
        /// Converts the relative Y or Height float to a scaled one.
        /// </summary>
        /// <param name="y">The float to convert.</param>
        /// <returns>A scaled float.</returns>
        public static float ToYScaled(this float y) => 1080f * y;

        #endregion

        #region PointF

        /// <summary>
        /// Converts a scaled 1080p-based position into a relative one.
        /// </summary>
        /// <param name="point">The scaled PointF.</param>
        /// <returns>A new PointF with relative values.</returns>
        public static PointF ToRelative(this PointF point) => new PointF(point.X.ToXRelative(), point.Y.ToYRelative());
        /// <summary>
        /// Converts a relative 0-1 position into a scaled one.
        /// </summary>
        /// <param name="point">The relative PointF.</param>
        /// <returns>A new PointF with scaled values.</returns>
        public static PointF ToScaled(this PointF point) => new PointF(point.X.ToXScaled(), point.Y.ToYScaled());

        #endregion

        #region SizeF

        /// <summary>
        /// Converts a scaled 1080p-based position into a relative one.
        /// </summary>
        /// <param name="size">The scaled SizeF.</param>
        /// <returns>A new SizeF with relative values.</returns>
        public static SizeF ToRelative(this SizeF size) => new SizeF(size.Width.ToXRelative(), size.Height.ToYRelative());
        /// <summary>
        /// Converts a relative 0-1 position into a scaled one.
        /// </summary>
        /// <param name="size">The relative SizeF.</param>
        /// <returns>A new SizeF with scaled values.</returns>
        public static SizeF ToScaled(this SizeF size) => new SizeF(size.Width.ToXScaled(), size.Height.ToYScaled());

        #endregion
    }
}
