namespace LemonUI.Menus
{
    /// <summary>
    /// Represents the arguments of an item activation.
    /// </summary>
    public class ItemActivatedArgs
    {
        #region Properties

        /// <summary>
        /// The item that was just activated.
        /// </summary>
        public NativeItem Item { get; }

        #endregion

        #region Constructors

        internal ItemActivatedArgs(NativeItem item)
        {
            Item = item;
        }

        #endregion
    }
}
