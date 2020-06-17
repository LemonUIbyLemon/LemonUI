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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">The Safezone information.</param>
    public delegate void SafeZoneChangedEventHandler(object sender, SafeZoneChangedEventArgs e);

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
    /// <summary>
    /// Represents the changes of the Safezone size in the Display settings.
    /// </summary>
    public class SafeZoneChangedEventArgs
    {
        /// <summary>
        /// The raw Safezone size before the change.
        /// </summary>
        public float Before { get; }
        /// <summary>
        /// The Safezone size after the change.
        /// </summary>
        public float After { get; }

        public SafeZoneChangedEventArgs(float before, float after)
        {
            Before = before;
            After = after;
        }
    }

    #endregion
}
