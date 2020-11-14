using LemonUI.Elements;
using System;
using System.Drawing;

namespace LemonUI.Menus
{
    /// <summary>
    /// Basic Rockstar-like item.
    /// </summary>
    public class NativeItem : IDrawable
    {
        #region Protected Internal Fields

        /// <summary>
        /// The title of the object.
        /// </summary>
        protected internal ScaledText title = null;
        /// <summary>
        /// The last known Item Position.
        /// </summary>
        protected internal PointF lastPosition = PointF.Empty;
        /// <summary>
        /// The last known Item Size.
        /// </summary>
        protected internal SizeF lastSize = SizeF.Empty;
        /// <summary>
        /// The last known Item Selection.
        /// </summary>
        protected internal bool lastSelected = false;
        /// <summary>
        /// The left badge of the Item.
        /// </summary>
        protected internal I2Dimensional badgeLeft = null;
        /// <summary>
        /// The left badge of the Item.
        /// </summary>
        protected internal I2Dimensional badgeRight = null;
        /// <summary>
        /// The alternate title of the menu.
        /// </summary>
        protected internal ScaledText altTitle = null;

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
                if (enabled == value)
                {
                    return;
                }
                enabled = value;
                EnabledChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// Object that contains data about this Item.
        /// </summary>
        public virtual object Tag { get; set; }
        /// <summary>
        /// The title of the item.
        /// </summary>
        public string Title
        {
            get => title.Text;
            set => title.Text = value;
        }
        /// <summary>
        /// The alternative title of the item shown on the right.
        /// </summary>
        public string AltTitle
        {
            get => altTitle.Text;
            set => altTitle.Text = value;
        }
        /// <summary>
        /// The description of the item.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The Left badge of the Item.
        /// </summary>
        public I2Dimensional LeftBadge
        {
            get => badgeLeft;
            set
            {
                badgeLeft = value;
                Recalculate();
            }
        }
        /// <summary>
        /// The Right badge of the Item.
        /// </summary>
        public I2Dimensional RightBadge
        {
            get => badgeRight;
            set
            {
                badgeRight = value;
                Recalculate();
            }
        }
        /// <summary>
        /// The Panel asociated to this <see cref="NativeItem"/>.
        /// </summary>
        public NativePanel Panel { get; set; } = null;

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
        /// <summary>
        /// Event triggered when the <see cref="Enabled"/> property is changed.
        /// </summary>
        public event EventHandler EnabledChanged;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="NativeItem"/>.
        /// </summary>
        /// <param name="title">The title of the item.</param>
        public NativeItem(string title) : this(title, "", "")
        {
        }
        /// <summary>
        /// Creates a new <see cref="NativeItem"/>.
        /// </summary>
        /// <param name="title">The title of the item.</param>
        /// <param name="description">The description of the item.</param>
        public NativeItem(string title, string description) : this(title, description, "")
        {
        }
        /// <summary>
        /// Creates a new <see cref="NativeItem"/>.
        /// </summary>
        /// <param name="title">The title of the item.</param>
        /// <param name="description">The description of the item.</param>
        /// <param name="altTitle">The alternative title of the item, shown on the right.</param>
        public NativeItem(string title, string description, string altTitle)
        {
            this.title = new ScaledText(PointF.Empty, title, 0.345f)
            {
                Color = NativeMenu.colorWhiteSmoke
            };
            Description = description;
            this.altTitle = new ScaledText(PointF.Empty, altTitle, 0.345f)
            {
                Color = NativeMenu.colorWhiteSmoke
            };
            EnabledChanged += NativeItem_EnabledChanged;
        }

        #endregion

        #region Local Events

        private void NativeItem_EnabledChanged(object sender, EventArgs e)
        {
            title.Color = Enabled ? NativeMenu.colorWhite : NativeMenu.colorDisabled;
            altTitle.Color = Enabled ? NativeMenu.colorWhite : NativeMenu.colorDisabled;
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

        #region Private Functions

        /// <summary>
        /// Recalculates the item with the last known values.
        /// </summary>
        protected void Recalculate() => Recalculate(lastPosition, lastSize, lastSelected);

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
            // Save the values for later use
            lastPosition = pos;
            lastSize = size;
            lastSelected = selected;

            // If there is a left badge, set the size and position
            if (badgeLeft != null)
            {
                badgeLeft.Position = new PointF(pos.X + 2, pos.Y - 3);
                badgeLeft.Size = new SizeF(45, 45);
            }
            if (badgeRight != null)
            {
                badgeRight.Position = new PointF(pos.X + size.Width - 47, pos.Y - 3);
                badgeRight.Size = new SizeF(45, 45);
            }
            // Just set the color and position of the title
            if (!Enabled)
            {
                title.Color = NativeMenu.colorDisabled;
                altTitle.Color = NativeMenu.colorDisabled;
            }
            else if (selected)
            {
                title.Color = NativeMenu.colorBlack;
                altTitle.Color = NativeMenu.colorBlack;
            }
            else
            {
                title.Color = NativeMenu.colorWhiteSmoke;
                altTitle.Color = NativeMenu.colorWhiteSmoke;
            }
            title.Position = new PointF(pos.X + (badgeLeft == null ? 0 : 34) + 6, pos.Y + 3);
            altTitle.Position = new PointF(pos.X + size.Width - (badgeRight == null ? 0 : 34) - altTitle.Width - 6, pos.Y + 3);
        }
        /// <summary>
        /// Draws the item.
        /// </summary>
        public virtual void Draw()
        {
            title.Draw();
            altTitle.Draw();
            badgeLeft?.Draw();
            badgeRight?.Draw();
        }

        #endregion
    }
}
