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
        protected internal ScaledTexture check = new ScaledTexture(PointF.Empty, SizeF.Empty, "commonmenu", string.Empty);
        /// <summary>
        /// If this item is checked or not.
        /// </summary>
        private bool checked_ = false;

        #endregion

        #region Defaults

        /// <summary>
        /// The default checkbox textures when the checkbox is checked.
        /// </summary>
        public static readonly BadgeSet DefaultCheckedSet = new BadgeSet
        {
            NormalDictionary = "commonmenu",
            NormalTexture = "shop_box_tick",
            HoveredDictionary = "commonmenu",
            HoveredTexture = "shop_box_tickb"
        };
        /// <summary>
        /// The default checkbox textures when the checkbox is not checked.
        /// </summary>
        public static readonly BadgeSet DefaultUncheckedSet = new BadgeSet
        {
            NormalDictionary = "commonmenu",
            NormalTexture = "shop_box_blank",
            HoveredDictionary = "commonmenu",
            HoveredTexture = "shop_box_blankb"
        };

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
        /// <summary>
        /// The textures used when the checkbox is checked.
        /// </summary>
        public BadgeSet CheckedSet { get; set; } = DefaultCheckedSet;
        /// <summary>
        /// The textures used when the checkbox is unchecked.
        /// </summary>
        public BadgeSet UncheckedSet { get; set; } = DefaultUncheckedSet;

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
        public NativeCheckboxItem(string title) : this(title, string.Empty, false)
        {
        }
        /// <summary>
        /// Creates a new <see cref="NativeCheckboxItem"/>.
        /// </summary>
        /// <param name="title">The title used for the Item.</param>
        /// <param name="check">If the checkbox should be enabled or not.</param>
        public NativeCheckboxItem(string title, bool check) : this(title, string.Empty, check)
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

        #region Event Functions

        private void NativeCheckboxItem_EnabledChanged(object sender, EventArgs e) => UpdateTexture(lastSelected);

        #endregion

        #region Tools

        /// <summary>
        /// Inverts the checkbox activation.
        /// </summary>
        private void Toggle(object sender, EventArgs e) => Checked = !Checked;
        /// <summary>
        /// Updates the texture of the sprite.
        /// </summary>
        protected internal void UpdateTexture(bool selected)
        {
            bool showLight = !selected || !Enabled;

            // If the item is not selected or is not enabled, use the white pictures
            if (Checked)
            {
                check.Texture = showLight ? CheckedSet.NormalTexture : CheckedSet.HoveredTexture;
            }
            // Otherwise, use the black ones
            else
            {
                check.Texture = showLight ? UncheckedSet.NormalTexture : UncheckedSet.HoveredTexture;
            }
        }

        #endregion

        #region Functions

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
            if (UseCustomBackground)
            {
                background.Draw();
            }

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
