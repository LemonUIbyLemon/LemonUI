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
            Resolution.ToRelative(fin, 0, out float fout, out _);
            return fout;
        }

        /// <summary>
        /// Converts an absolute Y or Height float to a relative one.
        /// </summary>
        /// <param name="fin">The float to convert.</param>
        /// <returns>A relative float between 0 and 1.</returns>
        public static float ToYRelative(this float fin)
        {
            Resolution.ToRelative(0, fin, out _, out float fout);
            return fout;
        }
    }
}
