namespace LemonUI.Menus
{
    /// <summary>
    /// Represents the method that is called when an item is added to or removed from a List Item.
    /// </summary>
    /// <typeparam name="T">The type of item that was modified.</typeparam>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="ItemModifiedEventArgs{T}"/> with the information of the modified item.</param>
    public delegate void ItemModifiedEventHandler<T>(object sender, ItemModifiedEventArgs<T> e);
}