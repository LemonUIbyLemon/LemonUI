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
    /// <summary>
    /// Event triggered when a specific item is selected.
    /// </summary>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">The index information.</param>
    public delegate void SelectedEventHandler(object sender, SelectedEventArgs e);

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
    /// <summary>
    /// Represents the selection of an item in the screen.
    /// </summary>
    public class SelectedEventArgs
    {
        /// <summary>
        /// The index of the item in the full list of items.
        /// </summary>
        public int Index { get; }
        /// <summary>
        /// The index of the item in the screen.
        /// </summary>
        public int OnScreen { get; }

        public SelectedEventArgs(int index, int screen)
        {
            Index = index;
            OnScreen = screen;
        }
    }

    #endregion
}
