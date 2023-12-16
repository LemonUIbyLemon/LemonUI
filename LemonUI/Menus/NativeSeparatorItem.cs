#if FIVEM
using CitizenFX.Core.UI;
#elif RAGEMP
using RAGE.Game;
#elif SHVDN3 || SHVDNC
using GTA.UI;
#endif
using System.Drawing;

namespace LemonUI.Menus
{
    /// <summary>
    /// An item used to have a space between the items with text or no text.
    /// </summary>
    public class NativeSeparatorItem : NativeItem
    {
        #region Constructors

        /// <summary>
        /// Creates a new separator.
        /// </summary>
        public NativeSeparatorItem() : this(string.Empty)
        {
        }
        /// <summary>
        /// Creates a new separator with a specific title.
        /// </summary>
        /// <param name="title">The title of the item.</param>
        public NativeSeparatorItem(string title) : base(title, string.Empty, string.Empty)
        {
            this.title.Alignment = Alignment.Center;
        }

        #endregion

        #region Functions

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
