namespace LemonUI.Menus
{
    /// <summary>
    /// Represents the method that is called when a new item is selected in the Menu.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="SelectedEventArgs"/> with the index information.</param>
    public delegate void SelectedEventHandler(object sender, SelectedEventArgs e);
}
