namespace LemonUI.Extensions
{
    /// <summary>
    /// Extensions for the float class.
    /// </summary>
    public static class FloatExtensions
    {
        /// <summary>
        /// Converts an absolute X or Width float to a relative one.
        /// </summary>
        /// <param name="fin">The float to convert.</param>
        /// <returns>A relative float between 0 and 1.</returns>
        public static float ToXRelative(this float fin)
        {
            Screen.ToRelative(fin, 0, out float fout, out _);
            return fout;
        }
        /// <summary>
        /// Converts an absolute Y or Height float to a relative one.
        /// </summary>
        /// <param name="fin">The float to convert.</param>
        /// <returns>A relative float between 0 and 1.</returns>
        public static float ToYRelative(this float fin)
        {
            Screen.ToRelative(0, fin, out _, out float fout);
            return fout;
        }
        /// <summary>
        /// Converts an relative X or Width float to an absolute one.
        /// </summary>
        /// <param name="fin">The float to convert.</param>
        /// <returns>An absolute float.</returns>
        public static float ToXAbsolute(this float fin)
        {
            Screen.ToAbsolute(fin, 0, out float fout, out _);
            return fout;
        }
        /// <summary>
        /// Converts an relative Y or Height float to an absolute one.
        /// </summary>
        /// <param name="fin">The float to convert.</param>
        /// <returns>An absolute float.</returns>
        public static float ToYAbsolute(this float fin)
        {
            Screen.ToAbsolute(0, fin, out _, out float fout);
            return fout;
        }
    }
}
