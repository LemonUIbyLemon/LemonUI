namespace LemonUI
{
    /// <summary>
    /// Represents the information after a Safe Zone Change in the game.
    /// </summary>
    public class SafeZoneChangedEventArgs
    {
        #region Properties

        /// <summary>
        /// The raw Safezone size before the change.
        /// </summary>
        public float Before { get; }
        /// <summary>
        /// The Safezone size after the change.
        /// </summary>
        public float After { get; }

        #endregion

        #region Constructors

        internal SafeZoneChangedEventArgs(float before, float after)
        {
            Before = before;
            After = after;
        }

        #endregion
    }
}
