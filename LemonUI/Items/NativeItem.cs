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
        /// <summary>
        /// Event triggered when the item is activated.
        /// </summary>
        public event EventHandler Activated;

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
        /// <summary>
        /// Triggers the Activated event.
        /// </summary>
        protected internal void OnActivated(object sender) => Activated?.Invoke(sender, EventArgs.Empty);

        #endregion

        #region Public Functions

        /// <summary>
        /// Recalculates the item positions and sizes with the specified values.
        /// </summary>
        /// <param name="pos">The position of the item.</param>
        /// <param name="size">The size of the item.</param>
        /// <param name="selected">If this item has been selected.</param>
        public virtual void Recalculate(PointF pos, SizeF size, bool selected)
        {
            // Just set the color and position of the title
            if (!Enabled)
            {
                title.Color = NativeMenu.colorDisabled;
            }
            else if (selected)
            {
                title.Color = NativeMenu.colorBlack;
            }
            else
            {
                title.Color = NativeMenu.colorWhiteSmoke;
            }
            title.Position = new PointF(pos.X + 6, pos.Y + 3);
        }
        /// <summary>
        /// Draws the item.
        /// </summary>
        public virtual void Draw() => title?.Draw();

        #endregion
    }
}
