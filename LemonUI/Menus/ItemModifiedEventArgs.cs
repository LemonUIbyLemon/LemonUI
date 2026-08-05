namespace LemonUI.Menus
{
    /// <summary>
    /// Represents the addition or removal of an item in a list.
    /// </summary>
    /// <typeparam name="T">The type of object that was modified.</typeparam>
    public class ItemModifiedEventArgs<T>
    {
        #region Properties

        /// <summary>
        /// The item that was added or removed.
        /// </summary>
        public T Item { get; }
        
        /// <summary>
        /// The position where the item was added or removed.
        /// </summary>
        public int Position { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="ItemModifiedEventArgs{T}"/>.
        /// </summary>
        /// <param name="item">The item that was added or removed.</param>
        /// <param name="position">The position where the item was added or removed.</param>
        public ItemModifiedEventArgs(T item, int position)
        {
            Item = item;
            Position = position;
        }

        #endregion
    }
}