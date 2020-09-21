using LemonUI.Elements;
using System;
using System.Drawing;

namespace LemonUI.Menus
{
    /// <summary>
    /// Basic elements for a slidable item.
    /// </summary>
    public abstract class NativeSlidableItem : NativeItem
    {
        #region Private Fields

        private bool alwaysVisible = false;

        #endregion

        #region Internal Fields

        /// <summary>
        /// The arrow pointing to the Left.
        /// </summary>
        [Obsolete("arrowLeft is Obsolete, use LeftArrow instead.")]
        internal protected ScaledTexture arrowLeft = null;
        /// <summary>
        /// The arrow pointing to the Right.
        /// </summary>
        [Obsolete("arrowRight is Obsolete, use RightArrow instead.")]
        internal protected ScaledTexture arrowRight = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// The arrow pointing to the Left.
        /// </summary>
        public ScaledTexture LeftArrow { get; }
        /// <summary>
        /// The arrow pointing to the Right.
        /// </summary>
        public ScaledTexture RightArrow { get; }
        /// <summary>
        /// Whether the arrows should always be shown regardless of the visibility of the Item.
        /// </summary>
        public bool ArrowsAlwaysVisible
        {
            get => alwaysVisible;
            set
            {
                alwaysVisible = value;
                Recalculate();
            }
        }

        #endregion
        
        #region Constructors

        /// <summary>
        /// Creates a new item that can be sliden.
        /// </summary>
        /// <param name="title">The title of the Item.</param>
        /// <param name="description">The description of the Item.</param>
        public NativeSlidableItem(string title, string description) : base(title, description)
        {
            LeftArrow = new ScaledTexture(PointF.Empty, SizeF.Empty, "commonmenu", "arrowleft");
            RightArrow = new ScaledTexture(PointF.Empty, SizeF.Empty, "commonmenu", "arrowright");
            arrowLeft = LeftArrow;
            arrowRight = RightArrow;
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
            // Set the sizes of the arrows
            LeftArrow.Size = (selected && Enabled) || ArrowsAlwaysVisible ? new SizeF(30, 30) : SizeF.Empty;
            LeftArrow.Color = selected ? NativeMenu.colorBlack : NativeMenu.colorWhiteSmoke;
            RightArrow.Size = (selected && Enabled) || ArrowsAlwaysVisible ? new SizeF(30, 30) : SizeF.Empty;
            RightArrow.Color = selected ? NativeMenu.colorBlack : NativeMenu.colorWhiteSmoke;
            // And set the positions of the right arrow
            RightArrow.Position = new PointF(pos.X + size.Width - RightArrow.Size.Width - 5, pos.Y + 4);
        }
        /// <summary>
        /// Moves to the previous item.
        /// </summary>
        public abstract void GoLeft();
        /// <summary>
        /// Moves to the next item.
        /// </summary>
        public abstract void GoRight();
        /// <summary>
        /// Draws the left and right arrow.
        /// </summary>
        public override void Draw()
        {
            title.Draw();
            badgeLeft?.Draw();
            LeftArrow.Draw();
            RightArrow.Draw();
        }

        #endregion
    }
}
