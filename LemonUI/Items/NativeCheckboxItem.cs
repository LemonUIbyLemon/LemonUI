using System;

namespace LemonUI.Items
{
    /// <summary>
    /// Rockstar-like checkbox item.
    /// </summary>
    public class NativeCheckboxItem : NativeItem, ICheckboxItem
    {
        #region Fields

        /// <summary>
        /// If this item is checked or not.
        /// </summary>
        private bool checked_ = false;

        #endregion

        #region Properties

        /// <summary>
        /// If this item is checked or not.
        /// </summary>
        public bool Checked
        {
            get => checked_;
            set
            {
                if (checked_ != value)
                {
                    CheckboxChanged?.Invoke(this, EventArgs.Empty);
                }
                checked_ = value;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when the checkbox changes.
        /// </summary>
        public event EventHandler CheckboxChanged;

        #endregion

        #region Constructor

        public NativeCheckboxItem(string title) : this(title, "", false)
        {
        }

        public NativeCheckboxItem(string title, bool check) : this(title, "", check)
        {
        }

        public NativeCheckboxItem(string title, string description) : this(title, description, false)
        {
        }

        public NativeCheckboxItem(string title, string description, bool check) : base(title, description)
        {
            Checked = check;
        }

        #endregion
    }
}
