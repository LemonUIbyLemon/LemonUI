using System;

namespace LemonUI.Menus
{
    /// <summary>
    /// Item used for opening submenus.
    /// </summary>
    public class NativeSubmenuItem : NativeItem
    {
        #region Public Properties

        /// <summary>
        /// The menu opened by this item.
        /// </summary>
        public NativeMenu Menu { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new Item that opens a Submenu.
        /// </summary>
        /// <param name="menu">The menu that this item will open.</param>
        /// <param name="parent">The parent menu where this item will be located.</param>
        public NativeSubmenuItem(NativeMenu menu, NativeMenu parent) : this(menu, parent, ">>>")
        {
        }
        /// <summary>
        /// Creates a new Item that opens a Submenu.
        /// </summary>
        /// <param name="menu">The menu that this item will open.</param>
        /// <param name="parent">The parent menu where this item will be located.</param>
        /// <param name="endlabel">The alternative title of the item, shown on the right.</param>
        public NativeSubmenuItem(NativeMenu menu, NativeMenu parent, string endlabel) : base(menu.Subtitle, menu.Description, endlabel)
        {
            Menu = menu ?? throw new ArgumentNullException(nameof(menu));
            Menu.Parent = parent ?? throw new ArgumentNullException(nameof(parent));

            Activated += NativeSubmenuItem_Activated;
        }

        #endregion

        #region Local Events

        private void NativeSubmenuItem_Activated(object sender, EventArgs e)
        {
            // Try to close the parent menu
            Menu.Parent.Visible = false;
            // And show the menu only if the parent menu is closed
            if (!Menu.Parent.Visible)
            {
                Menu.Visible = true;
            }
        }

        #endregion
    }
}
