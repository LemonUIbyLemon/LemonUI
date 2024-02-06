using System;

namespace LemonUI.Menus
{
    /// <summary>
    /// Item used for opening submenus.
    /// </summary>
    public class NativeSubmenuItem : NativeItem
    {
        #region Properties

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
        /// <param name="endLabel">The alternative title of the item, shown on the right.</param>
        public NativeSubmenuItem(NativeMenu menu, NativeMenu parent, string endLabel) : base(menu.Name, menu.Description, endLabel)
        {
            Menu = menu ?? throw new ArgumentNullException(nameof(menu));
            Menu.Parent = parent ?? throw new ArgumentNullException(nameof(parent));

            Activated += NativeSubmenuItem_Activated;
        }

        #endregion

        #region Event Functions

        private void NativeSubmenuItem_Activated(object sender, EventArgs e)
        {
            Menu.Parent.Visible = false;

            if (!Menu.Parent.Visible)
            {
                Menu.Visible = true;
            }
        }

        #endregion

        #region Functions

        /// <inheritdoc/>
        public override void Draw()
        {
            if (Description != Menu.Description)
            {
                Description = Menu.Description;
            }

            base.Draw();
        }

        #endregion
    }
}
