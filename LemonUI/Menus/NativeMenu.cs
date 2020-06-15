using LemonUI.Elements;
using LemonUI.Items;
using LemonUI.Menus;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LemonUI.NativeMenu
{
    /// <summary>
    /// Menu that looks like the ones used by Rockstar.
    /// </summary>
    public class NativeMenu : INativeMenu
    {
        #region Private Fields

        /// <summary>
        /// If the menu is visible or not.
        /// </summary>
        private bool visible = false;

        #endregion

        #region Public Properties

        /// <summary>
        /// If the menu is visible on the screen.
        /// </summary>
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
                if (visible)
                {
                    Close();
                }
                else
                {
                    Shown?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// The title of the menu.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The current index of the menu.
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// The items that this menu contain.
        /// </summary>
        public List<IItem> Items { get; }
        /// <summary>
        /// The banner shown at the top of the menu.
        /// </summary>
        public IDrawable Banner { get; set; }
        /// <summary>
        /// Text shown when there are no items in the menu.
        /// </summary>
        public string NoItemsText { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when the menu is opened and shown to the user.
        /// </summary>
        public event EventHandler Shown;
        /// <summary>
        /// Event triggered when the menu starts closing.
        /// </summary>
        public event CancelEventHandler Closing;
        /// <summary>
        /// Event triggered when the menu finishes closing.
        /// </summary>
        public event EventHandler Closed;
        /// <summary>
        /// Event triggered when an item is selected.
        /// </summary>
        public event EventHandler Selected;
        /// <summary>
        /// Event triggered when the selected index has been changed.
        /// </summary>
        public event EventHandler ItemChanged;
        /// <summary>
        /// Event triggered when the index has been changed.
        /// </summary>
        public event EventHandler IndexChanged;

        #endregion

        #region Constructor

        public NativeMenu(string title)
        {
            // Just save the title
            Title = title;
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Closes the menu.
        /// </summary>
        public void Close()
        {
            // Create a new set of event arguments
            CancelEventArgs args = new CancelEventArgs();
            // And trigger the event
            Closed?.Invoke(this, args);

            // If we need to cancel the closure of the menu, return
            if (args.Cancel)
            {
                return;
            }
            // Otherwise, hide the menu
            visible = false;
        }

        #endregion
    }
}
