namespace LemonUI.Menus
{
    /// <summary>
    /// Represents the arguments of an item activation.
    /// </summary>
    public class ItemActivatedArgs
    {
        /// <summary>
        /// The item that was just activated.
        /// </summary>
        public NativeItem Item { get; }

        internal ItemActivatedArgs(NativeItem item)
        {
            Item = item;
        }
    }
}
