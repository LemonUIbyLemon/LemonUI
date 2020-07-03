using LemonUI.Elements;
using LemonUI.Menus;
using System.Drawing;

namespace LemonUI.Items
{
    /// <summary>
    /// Basic elements for a slidable item.
    /// </summary>
    public abstract class NativeSlidableItem : NativeItem, ISlidableItem
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
            base.Draw();
            arrowLeft.Draw();
            arrowRight.Draw();
        }

        #endregion
    }
}
