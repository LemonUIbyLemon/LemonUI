namespace LemonUI.Menus
{
    /// <summary>
    /// Represents the method that is called when an item is activated on a menu.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An <see cref="ItemActivatedArgs"/> with the item information.</param>
    public delegate void ItemActivatedEventHandler(object sender, ItemActivatedArgs e);
}
