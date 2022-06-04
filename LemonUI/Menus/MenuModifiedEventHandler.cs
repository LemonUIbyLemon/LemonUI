namespace LemonUI.Menus
{
    /// <summary>
    /// Represents the method that is called when the items on a menu are changed (added or removed).
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An <see cref="MenuModifiedEventArgs"/> with the menu operation.</param>
    public delegate void MenuModifiedEventHandler(object sender, MenuModifiedEventArgs e);
}
