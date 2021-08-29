using LemonUI.Elements;
using System.Drawing;

namespace LemonUI.Menus
{
    /// <summary>
    /// Dynamic Items allow you to dynamically change the item shown to the user.
    /// </summary>
    /// <typeparam name="T">The type of item.</typeparam>
    public class NativeDynamicItem<T> : NativeSlidableItem
    {
        #region Fields

        private readonly ScaledText text = new ScaledText(PointF.Empty, "", 0.35f);
        private T item = default;

        #endregion

        #region Properties

        /// <summary>
        /// The currently selected item.
        /// </summary>
        public T SelectedItem
        {
            get => item;
            set
            {
                item = value;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when the user has changed the item.
        /// </summary>
        public event ItemChangedEventHandler<T> ItemChanged;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new Dynamic List Item.
        /// </summary>
        /// <param name="title">The Title of the item.</param>
        public NativeDynamicItem(string title) : this(title, "", default)
        {
        }
        /// <summary>
        /// Creates a new Dynamic List Item.
        /// </summary>
        /// <param name="title">The Title of the item.</param>
        /// <param name="item">The Item to set.</param>
        public NativeDynamicItem(string title, T item) : this(title, "", item)
        {
        }
        /// <summary>
        /// Creates a new Dynamic List Item.
        /// </summary>
        /// <param name="title">The Title of the item.</param>
        /// <param name="description">The Description of the item.</param>
        public NativeDynamicItem(string title, string description) : this(title, description, default)
        {
        }
        /// <summary>
        /// Creates a new Dynamic List Item.
        /// </summary>
        /// <param name="title">The Title of the item.</param>
        /// <param name="description">The Description of the item.</param>
        /// <param name="item">The Item to set.</param>
        public NativeDynamicItem(string title, string description, T item) : base(title, description)
        {
            this.item = item;
        }

        #endregion

        #region Functions

        /// <summary>
        /// Updates the currently selected item based on the index.
        /// </summary>
        private void UpdateItemName()
        {
            // This is the SAME as the normal NativeListItem

            text.Text = !SelectedItem.Equals(default) ? SelectedItem.ToString() : "";

            text.Position = new PointF(RightArrow.Position.X - text.Width + 3, text.Position.Y);
            LeftArrow.Position = new PointF(text.Position.X - LeftArrow.Size.Width, LeftArrow.Position.Y);
        }
        /// <summary>
        /// Gets the previous item.
        /// </summary>
        public override void GoLeft()
        {
            ItemChangedEventArgs<T> arguments = new ItemChangedEventArgs<T>(item, -1, Direction.Left);
            ItemChanged?.Invoke(this, arguments);
            SelectedItem = arguments.Object;
            UpdateItemName();
        }
        /// <summary>
        /// Gets the next item.
        /// </summary>
        public override void GoRight()
        {
            ItemChangedEventArgs<T> arguments = new ItemChangedEventArgs<T>(item, -1, Direction.Right);
            ItemChanged?.Invoke(this, arguments);
            SelectedItem = arguments.Object;
            UpdateItemName();
        }
        /// <summary>
        /// Recalculates the position of the current List Item.
        /// </summary>
        /// <param name="pos">The new position of the item.</param>
        /// <param name="size">The Size of the item.</param>
        /// <param name="selected">If the item is selected or not.</param>
        public override void Recalculate(PointF pos, SizeF size, bool selected)
        {
            // This is the SAME as the normal NativeListItem

            base.Recalculate(pos, size, selected);

            float textWidth = RightArrow.Size.Width;
            text.Position = new PointF(pos.X + size.Width - textWidth - 1 - text.Width, pos.Y + 3);
            LeftArrow.Position = new PointF(text.Position.X - LeftArrow.Size.Width, pos.Y + 4);

            UpdateItemName();
        }
        /// <summary>
        /// Draws the List on the screen.
        /// </summary>
        public override void Draw()
        {
            base.Draw(); // Arrows, Title and Left Badge
            text.Draw();
        }
        /// <inheritdoc/>
        public override void UpdateColors()
        {
            base.UpdateColors();

            if (!Enabled)
            {
                text.Color = Colors.TitleDisabled;
            }
            else if (lastSelected)
            {
                text.Color = Colors.TitleHovered;
            }
            else
            {
                text.Color = Colors.TitleNormal;
            }
        }

        #endregion
    }
}
