using System.Drawing;
using GTA.UI;

namespace LemonUI.Menus
{
    /// <summary>
    /// An item used to have a space between the items with text.
    /// </summary>
    public class NativeSpacerItem : NativeItem
    {
        #region Constructor

        /// <summary>
        /// Creates a new Spacer with no text.
        /// </summary>
        public NativeSpacerItem() : this(string.Empty)
        {
        }
        /// <summary>
        /// Creates a new Spacer with a specific title.
        /// </summary>
        /// <param name="title">The title of the item.</param>
        public NativeSpacerItem(string title) : base(title, string.Empty, string.Empty)
        {
            this.title.Alignment = Alignment.Center;
        }

        #endregion

        #region Function

        /// <inheritdoc/>
        public override void Recalculate(PointF pos, SizeF size, bool selected)
        {
            base.Recalculate(pos, size, selected);

            title.Position = new PointF(pos.X + (size.Width * 0.5f), title.Position.Y);
        }
        /// <inheritdoc/>
        public override void Draw()
        {
            title.Draw();
        }

        #endregion
    }
}
