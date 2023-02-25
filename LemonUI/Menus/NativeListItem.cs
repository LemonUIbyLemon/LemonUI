using LemonUI.Elements;
using System.Drawing;

namespace LemonUI.Menus
{
    /// <summary>
    /// Base class for list items.
    /// </summary>
    public abstract class NativeListItem : NativeSlidableItem
    {
        /// <summary>
        /// The text of the current item.
        /// </summary>
        internal protected ScaledText text = null;

        /// <summary>
        /// Creates a new list item with a title and subtitle.
        /// </summary>
        /// <param name="title">The title of the Item.</param>
        /// <param name="subtitle">The subtitle of the Item.</param>
        public NativeListItem(string title, string subtitle) : base(title, subtitle)
        {
            text = new ScaledText(PointF.Empty, string.Empty, 0.35f);
        }
    }
}
