using System.Drawing;

namespace LemonUI
{
    #region Delegates

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">The resolution information.</param>
    public delegate void ResolutionChangedEventHandler(object sender, ResolutionChangedEventArgs e);

    #endregion

    #region Arguments

    /// <summary>
    /// Represents information sent when the game resolution is changed.
    /// </summary>
    public class ResolutionChangedEventArgs
    {
        /// <summary>
        /// The resolution before the change;
        /// </summary>
        public SizeF Before { get; }
        /// <summary>
        /// The resolution after the change;
        /// </summary>
        public SizeF After { get; }

        public ResolutionChangedEventArgs(SizeF before, SizeF after)
        {
            Before = before;
            After = after;
        }
    }

    #endregion
}
