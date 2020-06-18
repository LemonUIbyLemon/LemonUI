using System;

namespace LemonUI.Items
{
    /// <summary>
    /// A standard menu item.
    /// </summary>
    public interface IItem : IDrawable
    {
        #region Public Properties
        
        /// <summary>
        /// If this item can be used or not.
        /// </summary>
        bool Enabled { get; set; }
        /// <summary>
        /// The title of this item.
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// The description of the item.
        /// </summary>
        string Description { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when the item is selected.
        /// </summary>
        event EventHandler Selected;

        #endregion
    }
}
