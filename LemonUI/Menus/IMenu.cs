using LemonUI.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LemonUI.Menus
{
    /// <summary>
    /// Base interface for generic menus that accept any type of item.
    /// </summary>
    public interface IMenu : IMenu<IItem>
    {
    }

    /// <summary>
    /// Base interface for implementing menus that accept specific items.
    /// </summary>
    public interface IMenu<T> : IRecalculable, IProcessable
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
        /// The items in this menu.
        /// </summary>
        List<T> Items { get; }

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
        /// Event triggered when the index has been changed.
        /// </summary>
        event EventHandler SelectedIndexChanged;

        #endregion

        #region Public Functions

        /// <summary>
        /// Adds a specific item to the menu.
        /// </summary>
        /// <param name="item">The item to add.</param>
        void Add(T item);
        /// <summary>
        /// Removes a specific item to the menu.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        void Remove(T item);
        /// <summary>
        /// Checks if an item is part of the menu.
        /// </summary>
        /// <param name="item">The item to check.</param>
        bool Contains(T item);
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
