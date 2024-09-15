#if FIVEMV2
using CitizenFX.FiveM.GUI;
using Font = CitizenFX.FiveM.GUI.Font;
#elif FIVEM
using Font = CitizenFX.Core.UI.Font;
#elif RAGEMP
using Font = RAGE.Game.Font;
#elif RPH
using Font = LemonUI.Elements.Font;
#elif SHVDN3 || SHVDNC
using Font = GTA.UI.Font;
#endif

using LemonUI.Elements;
using System;
using System.Drawing;
using LemonUI.Tools;

namespace LemonUI.Menus
{
    /// <summary>
    /// Basic Rockstar-like item.
    /// </summary>
    public class NativeItem : IDrawable
    {
        #region Fields

        /// <summary>
        /// The title of the object.
        /// </summary>
        protected internal ScaledText title;
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
        protected internal bool lastSelected;
        /// <summary>
        /// The left badge of the Item.
        /// </summary>
        protected internal I2Dimensional badgeLeft;
        /// <summary>
        /// The left badge of the Item.
        /// </summary>
        protected internal I2Dimensional badgeRight;
        /// <summary>
        /// The alternate title of the menu.
        /// </summary>
        protected internal ScaledText altTitle;

        private bool enabled = true;
        private BadgeSet badgeSetLeft;
        private BadgeSet badgeSetRight;
        private ColorSet colors = new ColorSet();
        internal ScaledRectangle background = new ScaledRectangle(PointF.Empty, SizeF.Empty);
        private string description = string.Empty;

        #endregion

        #region Properties

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
                UpdateColors();
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
            set => title.Text = value ?? throw new ArgumentNullException(nameof(value));
        }
        /// <summary>
        /// The alternative title of the item shown on the right.
        /// </summary>
        public string AltTitle
        {
            get => altTitle.Text;
            set
            {
                altTitle.Text = value ?? throw new ArgumentNullException(nameof(value));
                Recalculate();
            }
        }
        /// <summary>
        /// The font of title item.
        /// </summary>
        public Font TitleFont
        {
            get => title.Font;
            set => title.Font = value;
        }
        /// <summary>
        /// The font of alternative title item shown on the right.
        /// </summary>
        public Font AltTitleFont
        {
            get => altTitle.Font;
            set => altTitle.Font = value;
        }
        /// <summary>
        /// The description of the item.
        /// </summary>
        public string Description
        {
            get => description;
            set => description = value ?? throw new ArgumentNullException(nameof(value));
        }
        /// <summary>
        /// The Left badge of the Item.
        /// </summary>
        public I2Dimensional LeftBadge
        {
            get => badgeLeft;
            set
            {
                if (badgeLeft == value)
                {
                    return;
                }

                badgeLeft = value;

                Recalculate();
                UpdateColors();
            }
        }
        /// <summary>
        /// The Left badge set of the Item.
        /// </summary>
        public BadgeSet LeftBadgeSet
        {
            get => badgeSetLeft;
            set
            {
                if (badgeSetLeft == value)
                {
                    return;
                }

                badgeSetLeft = value;

                if (badgeSetLeft == null)
                {
                    badgeLeft = null;
                }

                Recalculate();
                UpdateColors();
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
                if (badgeRight == value)
                {
                    return;
                }

                badgeRight = value;

                Recalculate();
                UpdateColors();
            }
        }
        /// <summary>
        /// The Right badge set of the Item.
        /// </summary>
        public BadgeSet RightBadgeSet
        {
            get => badgeSetRight;
            set
            {
                if (badgeSetRight == value)
                {
                    return;
                }

                badgeSetRight = value;

                if (badgeSetRight == null)
                {
                    badgeRight = null;
                }

                Recalculate();
                UpdateColors();
            }
        }
        /// <summary>
        /// The different colors that change dynamically when the item is used.
        /// </summary>
        public ColorSet Colors
        {
            get => colors;
            set
            {
                colors = value;
                UpdateColors();
            }
        }
        /// <summary>
        /// The Panel associated to this <see cref="NativeItem"/>.
        /// </summary>
        public NativePanel Panel { get; set; } = null;
        /// <summary>
        /// If a custom colored background should be used.
        /// </summary>
        public bool UseCustomBackground { get; set; }
        /// <summary>
        /// If this item is being hovered.
        /// </summary>
        public bool IsHovered => GameScreen.IsCursorInArea(background.Position, background.Size);

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
        public NativeItem(string title) : this(title, string.Empty, string.Empty)
        {
        }
        /// <summary>
        /// Creates a new <see cref="NativeItem"/>.
        /// </summary>
        /// <param name="title">The title of the item.</param>
        /// <param name="description">The description of the item.</param>
        public NativeItem(string title, string description) : this(title, description, string.Empty)
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
            this.title = new ScaledText(PointF.Empty, title, 0.345f);
            Description = description;
            this.altTitle = new ScaledText(PointF.Empty, altTitle, 0.345f);
        }

        #endregion

        #region Tools

        /// <summary>
        /// Triggers the Selected event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="SelectedEventArgs"/> with the index information.</param>
        protected internal void OnSelected(object sender, SelectedEventArgs e) => Selected?.Invoke(sender, e);
        /// <summary>
        /// Triggers the Activated event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        protected internal void OnActivated(object sender) => Activated?.Invoke(sender, EventArgs.Empty);

        /// <summary>
        /// Recalculates the item with the last known values.
        /// </summary>
        protected void Recalculate() => Recalculate(lastPosition, lastSize, lastSelected);

        #endregion

        #region Functions

        /// <summary>
        /// Recalculates the item positions and sizes with the specified values.
        /// </summary>
        /// <param name="pos">The position of the item.</param>
        /// <param name="size">The size of the item.</param>
        /// <param name="selected">If this item has been selected.</param>
        public virtual void Recalculate(PointF pos, SizeF size, bool selected)
        {
            lastPosition = pos;
            lastSize = size;
            lastSelected = selected;

            background.Position = pos;
            background.Size = size;

            if (badgeSetLeft != null)
            {
                if (!(badgeLeft is ScaledTexture))
                {
                    badgeLeft = new ScaledTexture(string.Empty, string.Empty);
                }
                ScaledTexture left = (ScaledTexture)badgeLeft;
                left.Dictionary = selected ? badgeSetLeft.HoveredDictionary : badgeSetLeft.NormalDictionary;
                left.Texture = selected ? badgeSetLeft.HoveredTexture : badgeSetLeft.NormalTexture;
            }
            if (badgeSetRight != null)
            {
                if (!(badgeRight is ScaledTexture))
                {
                    badgeRight = new ScaledTexture(string.Empty, string.Empty);
                }
                ScaledTexture right = (ScaledTexture)badgeRight;
                right.Dictionary = selected ? badgeSetRight.HoveredDictionary : badgeSetRight.NormalDictionary;
                right.Texture = selected ? badgeSetRight.HoveredTexture : badgeSetRight.NormalTexture;
            }

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

            title.Position = new PointF(pos.X + (badgeLeft == null ? 0 : 34) + 6, pos.Y + 3);
            altTitle.Position = new PointF(pos.X + size.Width - (badgeRight == null ? 0 : 34) - altTitle.Width - 6, pos.Y + 3);

            UpdateColors();
        }
        /// <summary>
        /// Draws the item.
        /// </summary>
        public virtual void Draw()
        {
            if (UseCustomBackground)
            {
                background.Draw();
            }

            title.Draw();
            altTitle.Draw();
            badgeLeft?.Draw();
            badgeRight?.Draw();
        }
        /// <summary>
        /// Updates the colors of the <see cref="Elements"/> from the <see cref="Colors"/> <see cref="ColorSet"/>.
        /// </summary>
        public virtual void UpdateColors()
        {
            if (!Enabled)
            {
                background.Color = Colors.BackgroundDisabled;
                title.Color = Colors.TitleDisabled;
                altTitle.Color = Colors.AltTitleDisabled;
                if (badgeLeft != null)
                {
                    badgeLeft.Color = Colors.BadgeLeftDisabled;
                }
                if (badgeRight != null)
                {
                    badgeRight.Color = Colors.BadgeRightDisabled;
                }
            }
            else if (lastSelected && !(this is NativeSeparatorItem))
            {
                background.Color = Colors.BackgroundHovered;
                title.Color = Colors.TitleHovered;
                altTitle.Color = Colors.AltTitleHovered;
                if (badgeLeft != null)
                {
                    badgeLeft.Color = Colors.BadgeLeftHovered;
                }
                if (badgeRight != null)
                {
                    badgeRight.Color = Colors.BadgeRightHovered;
                }
            }
            else
            {
                background.Color = Colors.BackgroundNormal;
                title.Color = Colors.TitleNormal;
                altTitle.Color = Colors.AltTitleNormal;
                if (badgeLeft != null)
                {
                    badgeLeft.Color = Colors.BadgeLeftNormal;
                }
                if (badgeRight != null)
                {
                    badgeRight.Color = Colors.BadgeRightNormal;
                }
            }
        }

        #endregion
    }
}
