using LemonUI.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LemonUI.Menus
{
    /// <summary>
    /// Base interface for implementing menus.
    /// </summary>
    public interface IMenu : IRecalculable, IProcessable
    {
        #region Public Properties

        /// <summary>
        /// If the menu is visible on the screen.
        /// </summary>
        bool Visible { get; set; }
        /// <summary>
        /// The title of the menu.
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// The current index of the menu.
        /// </summary>
        int SelectedIndex { get; set; }
        /// <summary>
        /// The items that this menu contain.
        /// </summary>
        List<IItem> Items { get; }

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when the menu is opened and shown to the user.
        /// </summary>
        event EventHandler Shown;
        /// <summary>
        /// Event triggered when the menu starts closing.
        /// </summary>
        event CancelEventHandler Closing;
        /// <summary>
        /// Event triggered when the menu finishes closing.
        /// </summary>
        event EventHandler Closed;
        /// <summary>
        /// Event triggered when an item is selected.
        /// </summary>
        event EventHandler Selected;
        /// <summary>
        /// Event triggered when the selected index has been changed.
        /// </summary>
        event EventHandler SelectedItemChanged;
        /// <summary>
        /// Event triggered when the index has been changed.
        /// </summary>
        event EventHandler SelectedIndexChanged;

        #endregion

        #region Public Functions

        /// <summary>
        /// Switches to the previous item.
        /// </summary>
        void Previous();
        /// <summary>
        /// Switches to the next item.
        /// </summary>
        void Next();

        #endregion
    }
}
