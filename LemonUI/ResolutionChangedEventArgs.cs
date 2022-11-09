using System.Drawing;

namespace LemonUI
{
    /// <summary>
    /// Represents the information after a Resolution Change in the game.
    /// </summary>
    public class ResolutionChangedEventArgs
    {
        #region Properties

        /// <summary>
        /// The Game Resolution before it was changed.
        /// </summary>
        public SizeF Before { get; }
        /// <summary>
        /// The Game Resolution after it was changed.
        /// </summary>
        public SizeF After { get; }

        #endregion

        #region Constructors

        internal ResolutionChangedEventArgs(SizeF before, SizeF after)
        {
            Before = before;
            After = after;
        }

        #endregion
    }
}
