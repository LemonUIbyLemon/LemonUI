using LemonUI.Elements;
using LemonUI.Menus;
using System;
using System.Drawing;

namespace LemonUI.Items
{
    /// <summary>
    /// Basic Rockstar-like item.
    /// </summary>
    public class NativeItem : IItem
    {
        #region Protected Internal Fields

        /// <summary>
        /// The title of the object.
        /// </summary>
        protected internal ScaledText title = null;

        #endregion

        #region Private Fields

        /// <summary>
        /// If this item can be used or not.
        /// </summary>
        private bool enabled = true;

        #endregion

        #region Public Properties

        /// <summary>
        /// If this item can be used or not.
        /// </summary>
        public bool Enabled
        {
            get => enabled;
            set
            {
                // Save the value
                enabled = value;
                // And set the correct color for enabled and disabled
                title.Color = value ? NativeMenu.colorWhite : NativeMenu.colorDisabled;
            }
        }
        /// <summary>
        /// The title of the item.
        /// </summary>
        public string Title
        {
            get => title.Text;
            set => title.Text = value;
        }
        /// <summary>
        /// The description of the item.
        /// </summary>
        public string Description { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when the item is selected.
        /// </summary>
        public event SelectedEventHandler Selected;

        #endregion

        #region Constructors

        public NativeItem(string title) : this(title, "")
        {
        }

        public NativeItem(string title, string description)
        {
            this.title = new ScaledText(PointF.Empty, title, 0.345f)
            {
                Color = NativeMenu.colorWhiteSmoke
            };
            Description = description;
        }

        #endregion

        #region Event Triggers

        /// <summary>
        /// Triggers the Selected event.
        /// </summary>
        protected internal void OnSelected(object sender, SelectedEventArgs e) => Selected?.Invoke(sender, e);

        #endregion

        #region Public Functions

        /// <summary>
        /// Draws the item.
        /// </summary>
        public virtual void Draw() => title?.Draw();

        #endregion
    }
}
