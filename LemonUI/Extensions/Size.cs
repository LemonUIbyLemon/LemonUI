using System.Drawing;

namespace LemonUI.Extensions
{
    /// <summary>
    /// Extensions for the Size and SizeF classes.
    /// </summary>
    public static class SizeExtensions
    {
        /// <summary>
        /// Converts an absolute 1080-based size into a relative one.
        /// </summary>
        /// <param name="size">The absolute SizeF.</param>
        /// <returns>A new SizeF with relative values.</returns>
        public static SizeF ToRelative(this SizeF size)
        {
            Screen.ToRelative(size.Width, size.Height, out float width, out float height);
            return new SizeF(width, height);
        }
        /// <summary>
        /// Converts a normalized 0-1 size into an absolute one.
        /// </summary>
        /// <param name="size">The relative SizeF.</param>
        /// <returns>A new SizeF with absolute values.</returns>
        public static SizeF ToAbsolute(this SizeF size)
        {
            Screen.ToAbsolute(size.Width, size.Height, out float width, out float height);
            return new SizeF(width, height);
        }
    }
}
