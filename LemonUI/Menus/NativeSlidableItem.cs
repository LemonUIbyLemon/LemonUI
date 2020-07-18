using LemonUI.Elements;
using System.Drawing;

namespace LemonUI.Menus
{
    /// <summary>
    /// Basic elements for a slidable item.
    /// </summary>
    public abstract class NativeSlidableItem : NativeItem
    {
        #region Internal Fields

        /// <summary>
        /// The arrow pointing to the left.
        /// </summary>
        internal protected ScaledTexture arrowLeft = null;
        /// <summary>
        /// The arrow pointing to the right.
        /// </summary>
        internal protected ScaledTexture arrowRight = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new item that can be sliden.
        /// </summary>
        /// <param name="title">The title of the Item.</param>
        /// <param name="subtitle">The subtitle of the Item.</param>
        public NativeSlidableItem(string title, string subtitle) : base(title, subtitle)
        {
            arrowLeft = new ScaledTexture(PointF.Empty, SizeF.Empty, "commonmenu", "arrowleft")
            {
                Color = NativeMenu.colorBlack
            };
            arrowRight = new ScaledTexture(PointF.Empty, SizeF.Empty, "commonmenu", "arrowright")
            {
                Color = NativeMenu.colorBlack
            };
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
            arrowLeft.Size = selected && Enabled ? new SizeF(30, 30) : SizeF.Empty;
            arrowRight.Size = selected && Enabled ? new SizeF(30, 30) : SizeF.Empty;
            // And set the positions of the right arrow
            arrowRight.Position = new PointF(pos.X + size.Width - arrowRight.Size.Width - 5, pos.Y + 4);
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
            arrowLeft.Draw();
            arrowRight.Draw();
        }

        #endregion
    }
}
