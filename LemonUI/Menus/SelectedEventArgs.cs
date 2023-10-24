namespace LemonUI.Menus
{
    /// <summary>
    /// Represents the selection of an item in the screen.
    /// </summary>
    public class SelectedEventArgs
    {
        #region Properties

        /// <summary>
        /// The index of the item in the full list of items.
        /// </summary>
        public int Index { get; }
        /// <summary>
        /// The index of the item in the screen.
        /// </summary>
        public int OnScreen { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="SelectedEventArgs"/>.
        /// </summary>
        /// <param name="index">The index of the item in the menu.</param>
        /// <param name="screen">The index of the item based on the number of items shown on screen,</param>
        public SelectedEventArgs(int index, int screen)
        {
            Index = index;
            OnScreen = screen;
        }

        #endregion
    }
}
