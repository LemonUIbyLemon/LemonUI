using System;

namespace LemonUI.Items
{
    /// <summary>
    /// A standard menu item.
    /// </summary>
    public interface IItem : IProcessable
    {
        #region Public Properties

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
