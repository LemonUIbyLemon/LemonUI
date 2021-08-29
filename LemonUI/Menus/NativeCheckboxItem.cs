using LemonUI.Elements;
using System;
using System.Drawing;

namespace LemonUI.Menus
{
    /// <summary>
    /// Rockstar-like checkbox item.
    /// </summary>
    public class NativeCheckboxItem : NativeItem
    {
        #region Fields

        /// <summary>
        /// The image shown on the checkbox.
        /// </summary>
        internal protected ScaledTexture check = new ScaledTexture(PointF.Empty, SizeF.Empty, "commonmenu", "");
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
                if (checked_ == value)
                {
                    return;
                }
                checked_ = value;
                UpdateTexture(lastSelected);
                CheckboxChanged?.Invoke(this, EventArgs.Empty);
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

        /// <summary>
        /// Creates a new <see cref="NativeCheckboxItem"/>.
        /// </summary>
        /// <param name="title">The title used for the Item.</param>
        public NativeCheckboxItem(string title) : this(title, "", false)
        {
        }
        /// <summary>
        /// Creates a new <see cref="NativeCheckboxItem"/>.
        /// </summary>
        /// <param name="title">The title used for the Item.</param>
        /// <param name="check">If the checkbox should be enabled or not.</param>
        public NativeCheckboxItem(string title, bool check) : this(title, "", check)
        {
        }
        /// <summary>
        /// Creates a new <see cref="NativeCheckboxItem"/>.
        /// </summary>
        /// <param name="title">The title used for the Item.</param>
        /// <param name="description">The description of the Item.</param>
        public NativeCheckboxItem(string title, string description) : this(title, description, false)
        {
        }
        /// <summary>
        /// Creates a new <see cref="NativeCheckboxItem"/>.
        /// </summary>
        /// <param name="title">The title used for the Item.</param>
        /// <param name="description">The description of the Item.</param>
        /// <param name="check">If the checkbox should be enabled or not.</param>
        public NativeCheckboxItem(string title, string description, bool check) : base(title, description)
        {
            Checked = check;
            Activated += Toggle;
            EnabledChanged += NativeCheckboxItem_EnabledChanged;
        }

        #endregion

        #region Local Events

        private void NativeCheckboxItem_EnabledChanged(object sender, EventArgs e) => UpdateTexture(lastSelected);

        #endregion

        #region Internal Functions

        /// <summary>
        /// Inverts the checkbox activation.
        /// </summary>
        private void Toggle(object sender, EventArgs e) => Checked = !Checked;
        /// <summary>
        /// Updates the texture of the sprite.
        /// </summary>
        internal protected void UpdateTexture(bool selected)
        {
            // If the item is not selected or is not enabled, use the white pictures
            if (!selected || !Enabled)
            {
                check.Texture = Checked ? "shop_box_tick" : "shop_box_blank";
            }
            // Otherwise, use the black ones
            else
            {
                check.Texture = Checked ? "shop_box_tickb" : "shop_box_blankb";
            }
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Recalculates the item positions and sizes with the specified values.
        /// </summary>
        /// <param name="pos">The position of the item.</param>
        /// <param name="size">The size of the item.</param>
        /// <param name="selected">If this item has been selected.</param>
        public override void Recalculate(PointF pos, SizeF size, bool selected)
        {
            base.Recalculate(pos, size, selected);
            // Set the correct texture
            UpdateTexture(selected);
            // And set the checkbox positions
            check.Position = new PointF(pos.X + size.Width - 50, pos.Y - 6);
            check.Size = new SizeF(50, 50);
        }
        /// <summary>
        /// Draws the Checkbox on the screen.
        /// </summary>
        public override void Draw()
        {
            title.Draw();
            badgeLeft?.Draw();
            check.Draw();
        }
        /// <inheritdoc/>
        public override void UpdateColors()
        {
            base.UpdateColors();

            if (!Enabled)
            {
                check.Color = Colors.BadgeRightDisabled;
            }
            else if (lastSelected)
            {
                check.Color = Colors.BadgeRightHovered;
            }
            else
            {
                check.Color = Colors.BadgeRightNormal;
            }
        }

        #endregion
    }
}
