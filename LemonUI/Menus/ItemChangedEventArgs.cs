namespace LemonUI.Menus
{
    /// <summary>
    /// Represents the change of the selection of an item.
    /// </summary>
    /// <typeparam name="T">The type of object that got changed.</typeparam>
    public class ItemChangedEventArgs<T>
    {
        #region Properties

        /// <summary>
        /// The new object.
        /// </summary>
        public T Object { get; set; }
        /// <summary>
        /// The index of the object.
        /// </summary>
        public int Index { get; }
        /// <summary>
        /// The direction of the Item Changed event.
        /// </summary>
        public Direction Direction { get; }

        #endregion

        #region Constructors

        internal ItemChangedEventArgs(T obj, int index, Direction direction)
        {
            Object = obj;
            Index = index;
            Direction = direction;
        }

        #endregion
    }
}
