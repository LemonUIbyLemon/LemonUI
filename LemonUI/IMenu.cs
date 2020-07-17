#if SHVDN2 || SHVDN3
using CancelEventHandler = System.ComponentModel.CancelEventHandler;
#endif
using System;
using System.Collections.Generic;

namespace LemonUI
{
    /// <summary>
    /// Base interface for implementing menus that accept specific items.
    /// </summary>
    public interface IMenu<T> : IRecalculable, IProcessable
    {
        #region Public Properties

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
        event SelectedEventHandler SelectedIndexChanged;

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
