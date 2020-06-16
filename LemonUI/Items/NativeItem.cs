using System;

namespace LemonUI.Items
{
    /// <summary>
    /// Basic Rockstar-like item.
    /// </summary>
    public class NativeItem : IItem
    {
        #region Public Properties

        /// <summary>
        /// The title of the item.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The description of the item.
        /// </summary>
        public string Description { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when the item is selected.
        /// </summary>
        public event EventHandler Selected;

        #endregion

        #region Constructors

        public NativeItem(string title) : this(title, "")
        {
        }

        public NativeItem(string title, string description)
        {
            Title = title;
            Description = description;
        }

        #endregion
    }
}
