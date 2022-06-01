namespace LemonUI.Menus
{
    /// <summary>
    /// Represents the method that is called when the value on a grid is changed.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An <see cref="ItemActivatedArgs"/> with the item information.</param>
    public delegate void GridValueChangedEventHandler(object sender, GridValueChangedArgs e);
}
