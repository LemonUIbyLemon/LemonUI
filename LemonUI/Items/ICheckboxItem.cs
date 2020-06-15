using System;

namespace LemonUI.Items
{
    /// <summary>
    /// Interface for menu items that have checkboxes.
    /// </summary>
    public interface ICheckboxItem : IItem
    {
        #region Public Properties

        /// <summary>
        /// If the checkbox is checked or not.
        /// </summary>
        bool Checked { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when the checkbox status has changed.
        /// </summary>
        event EventHandler CheckboxChanged;

        #endregion
    }
}
