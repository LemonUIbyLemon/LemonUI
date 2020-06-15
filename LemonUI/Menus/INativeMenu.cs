using LemonUI.Elements;

namespace LemonUI.Menus
{
    /// <summary>
    /// Interface for menus that behave like the ones by Rockstar.
    /// </summary>
    public interface INativeMenu : IMenu
    {
        #region Public Properties

        /// <summary>
        /// The banner shown at the top of the menu.
        /// </summary>
        IDrawable Banner { get; set; }
        /// <summary>
        /// Text shown when there are no items in the menu.
        /// </summary>
        string NoItemsText { get; set; }

        #endregion
    }
}
