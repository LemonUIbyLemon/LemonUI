namespace LemonUI.TimerBars
{
    /// <summary>
    /// The spacing of the objectives in the timer bar.
    /// </summary>
    public enum ObjectiveSpacing
    {
        /// <summary>
        /// The objectives will be equally spaced.
        /// </summary>
        /// <remarks>
        /// If you have way too many objectives and not enough width, you might end up with objectives overlapping each other.
        /// </remarks>
        Equal = 0,
        /// <summary>
        /// The items will all have the same spacing.
        /// </summary>
        /// <remarks>
        /// If you have way too many objectives and not enough width, the objectives might end up outside of the timer bar.
        /// </remarks>
        Fixed = 1
    }
}
