namespace LemonUI.Menus
{
    /// <summary>
    /// A Blank Separator Item for creating empty spaces between menu items.
    /// </summary>
    public class NativeSeparatorItem : NativeItem
    {
        #region Constructor

        /// <summary>
        /// Creates a new Menu Separator.
        /// </summary>
        public NativeSeparatorItem() : base(string.Empty, string.Empty, string.Empty)
        {
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Draws nothing.
        /// </summary>
        public override void Draw()
        {
        }

        #endregion
    }
}
