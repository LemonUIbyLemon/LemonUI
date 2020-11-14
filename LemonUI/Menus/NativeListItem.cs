using LemonUI.Elements;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace LemonUI.Menus
{
    /// <summary>
    /// Represents the method that is called when the selected item is changed on a List Item.
    /// </summary>
    /// <typeparam name="T">The type of item that was changed.</typeparam>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="ItemChangedEventArgs{T}"/> with the information of the selected item.</param>
    public delegate void ItemChangedEventHandler<T>(object sender, ItemChangedEventArgs<T> e);

    /// <summary>
    /// The movement direction of the item change.
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// The Direction is Unknown.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// The item was moved to the Left.
        /// </summary>
        Left = 1,
        /// <summary>
        /// The item was moved to the Right.
        /// </summary>
        Right = 2,
    }

    /// <summary>
    /// Represents the change of the selection of an item.
    /// </summary>
    /// <typeparam name="T">The type of object that got changed.</typeparam>
    public class ItemChangedEventArgs<T>
    {
        /// <summary>
        /// The new object.
        /// </summary>
        public T Object { get; set; }
        /// <summary>
        /// The index of the object.
        /// </summary>
        public int Index { get; }
        /// <summary>
        /// The direction of the Item Changed event.
        /// </summary>
        public Direction Direction { get; }

        internal ItemChangedEventArgs(T obj, int index, Direction direction)
        {
            Object = obj;
            Index = index;
            Direction = direction;
        }
    }

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
            text = new ScaledText(PointF.Empty, "", 0.35f)
            {
                Color = NativeMenu.colorWhiteSmoke
            };
        }
    }

    /// <summary>
    /// An item that allows you to scroll between a set of objects.
    /// </summary>
    public class NativeListItem<T> : NativeListItem
    {
        #region Private Fields

        /// <summary>
        /// The index of the currently selected index.
        /// </summary>
        private int index = 0;
        /// <summary>
        /// The objects used by this item.
        /// </summary>
        private List<T> items = new List<T>();

        #endregion

        #region Public Properties

        /// <summary>
        /// The index of the currently selected index.
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                if (Items.Count == 0)
                {
                    return -1;
                }
                return index;
            }
            set
            {
                if (Items.Count == 0)
                {
                    throw new InvalidOperationException("There are no available items.");
                }
                if (value < 0)
                {
                    throw new InvalidOperationException("The index is under zero.");
                }
                if (value >= Items.Count)
                {
                    throw new InvalidOperationException($"The index is over the limit of {Items.Count - 1}");
                }
                if (index == value)
                {
                    return;
                }
                index = value;
                TriggerEvent(value, Direction.Unknown);
                UpdateIndex();
            }
        }
        /// <summary>
        /// The currently selected item.
        /// </summary>
        public T SelectedItem
        {
            get
            {
                if (Items.Count == 0)
                {
                    return default;
                }
                return Items[SelectedIndex];
            }
            set
            {
                if (Items.Count == 0)
                {
                    throw new InvalidOperationException("There are no available items.");
                }
                int index = Items.IndexOf(SelectedItem);
                if (index == -1)
                {
                    throw new InvalidOperationException("The object is not the list of Items.");
                }
                SelectedIndex = index;
            }
        }
        /// <summary>
        /// The objects used by this item.
        /// </summary>
        public List<T> Items
        {
            get => items;
            set
            {
                items = value;
                UpdateIndex();
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when the selected item is changed.
        /// </summary>
        public event ItemChangedEventHandler<T> ItemChanged;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="NativeListItem"/>.
        /// </summary>
        /// <param name="title">The title of the Item.</param>
        /// <param name="objs">The objects that are available on the Item.</param>
        public NativeListItem(string title, params T[] objs) : this(title, "", objs)
        {
        }
        /// <summary>
        /// Creates a new <see cref="NativeListItem"/>.
        /// </summary>
        /// <param name="title">The title of the Item.</param>
        /// <param name="subtitle">The subtitle of the Item.</param>
        /// <param name="objs">The objects that are available on the Item.</param>
        public NativeListItem(string title, string subtitle, params T[] objs) : base(title, subtitle)
        {
            // Create the basic stuff
            Items = new List<T>();
            // And add the objects passed
            Items.AddRange(objs);
            // Finally, update the visible item
            UpdateIndex();
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// Triggers the <seealso cref="ItemChangedEventHandler{T}"/> event.
        /// </summary>
        private void TriggerEvent(int index, Direction direction)
        {
            ItemChanged?.Invoke(this, new ItemChangedEventArgs<T>(items[index], index, direction));
        }
        /// <summary>
        /// Updates the currently selected item based on the index.
        /// </summary>
        private void UpdateIndex()
        {
            // Set the text based on the current item
            text.Text = SelectedIndex != -1 ? SelectedItem.ToString() : "";
            // And set the correct position
            text.Position = new PointF(RightArrow.Position.X - text.Width + 3, text.Position.Y);
            LeftArrow.Position = new PointF(text.Position.X - LeftArrow.Size.Width, LeftArrow.Position.Y);
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
            // Set the color of the selected item
            if (!Enabled)
            {
                text.Color = NativeMenu.colorDisabled;
            }
            else if (selected)
            {
                text.Color = NativeMenu.colorBlack;
            }
            else
            {
                text.Color = NativeMenu.colorWhiteSmoke;
            }
            // And set the position of the left arrow and text
            text.Position = new PointF(pos.X + size.Width - RightArrow.Size.Width - 1 - text.Width, pos.Y + 3);
            LeftArrow.Position = new PointF(text.Position.X - LeftArrow.Size.Width, pos.Y + 4);
        }
        /// <summary>
        /// Moves to the previous item.
        /// </summary>
        public override void GoLeft()
        {
            // If there are no items, return
            if (Items.Count == 0)
            {
                return;
            }

            // If this is the first item, go back to the last one
            if (index == 0)
            {
                index = Items.Count - 1;
            }
            // Otherwise, return to the previous one
            else
            {
                index--;
            }
            TriggerEvent(index, Direction.Left);
            UpdateIndex();
        }
        /// <summary>
        /// Moves to the next item.
        /// </summary>
        public override void GoRight()
        {
            // If there are no items, return
            if (Items.Count == 0)
            {
                return;
            }

            // If this is the last item, go back to the first one
            if (index == Items.Count - 1)
            {
                index = 0;
            }
            // Otherwise, continue to the next one
            else
            {
                index++;
            }
            TriggerEvent(index, Direction.Right);
            UpdateIndex();
        }
        /// <summary>
        /// Draws the List on the screen.
        /// </summary>
        public override void Draw()
        {
            base.Draw(); // Arrows, Title and Left Badge
            text.Draw();
        }

        #endregion
    }
}
