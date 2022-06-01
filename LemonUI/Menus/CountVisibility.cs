namespace LemonUI.Menus
{
    /// <summary>
    /// The visibility setting for the Item Count of the Menu.
    /// </summary>
    public enum CountVisibility
    {
        /// <summary>
        /// The Item Count is never shown.
        /// </summary>
        Never = -1,
        /// <summary>
        /// The Item Count is shown when is not possible to show all of the items in the screen.
        /// </summary>
        Auto = 0,
        /// <summary>
        /// The Item Count is always shown.
        /// </summary>
        Always = 1
    }
}
