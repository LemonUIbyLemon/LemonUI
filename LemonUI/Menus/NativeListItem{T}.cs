using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace LemonUI.Menus
{
    /// <summary>
    /// An item that allows you to scroll between a set of objects.
    /// </summary>
    public class NativeListItem<T> : NativeListItem, IEnumerable<T>
    {
        #region Fields

        private int index = 0;
        private List<T> items = new List<T>();

        #endregion

        #region Properties

        /// <summary>
        /// The index of the currently selected index.
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                if (items.Count == 0)
                {
                    return -1;
                }
                return index;
            }
            set
            {
                if (items.Count == 0)
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
                return Items.Count == 0 ? default : Items[index];
            }
            set
            {
                if (Items.Count == 0)
                {
                    return;
                }

                int newIndex = Items.IndexOf(value);

                if (newIndex == -1)
                {
                    throw new InvalidOperationException("The object is not the list of Items.");
                }

                SelectedIndex = newIndex;
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
        
        /// <summary>
        /// Event triggered when an item is added to the list.
        /// </summary>
        public event ItemModifiedEventHandler<T> ItemAdded;
        
        /// <summary>
        /// Event triggered when an item is removed from the list.
        /// </summary>
        public event ItemModifiedEventHandler<T> ItemRemoved;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="NativeListItem"/>.
        /// </summary>
        /// <param name="title">The title of the Item.</param>
        /// <param name="objs">The objects that are available on the Item.</param>
        public NativeListItem(string title, params T[] objs) : this(title, string.Empty, objs)
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
            items = new List<T>();
            items.AddRange(objs);
            UpdateIndex();
        }

        #endregion

        #region Tools

        /// <summary>
        /// Triggers the <seealso cref="ItemChangedEventHandler{T}"/> event.
        /// </summary>
        private void TriggerEvent(int index, Direction direction)
        {
            ItemChanged?.Invoke(this, new ItemChangedEventArgs<T>(items[index], index, direction));
        }
        private void FixIndexIfRequired()
        {
            if (items.Count == 0)
            {
                index = 0;
                UpdateIndex();
            }
            else if (index >= items.Count)
            {
                index = items.Count - 1;
                UpdateIndex();
            }
        }
        /// <summary>
        /// Updates the currently selected item based on the index.
        /// </summary>
        private void UpdateIndex()
        {
            text.Text = SelectedItem == null ? string.Empty : SelectedItem.ToString();

            text.Position = new PointF(RightArrow.Position.X - text.Width + 3, text.Position.Y);
            LeftArrow.Position = new PointF(text.Position.X - LeftArrow.Size.Width, LeftArrow.Position.Y);
        }

        #endregion

        #region Functions

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();
        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        /// <summary>
        /// Adds a <typeparamref name="T" /> into this item.
        /// </summary>
        /// <param name="item">The <typeparamref name="T" /> to add.</param>
        public void Add(T item) => Add(items.Count, item);
        /// <summary>
        /// Adds a <typeparamref name="T" /> in a specific location.
        /// </summary>
        /// <param name="position">The position where the item should be added.</param>
        /// <param name="item">The <typeparamref name="T" /> to add.</param>
        public void Add(int position, T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (position < 0 || position > Items.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(position), "The position is out of the range of items.");
            }

            items.Insert(position, item);

            FixIndexIfRequired();

            if (items.Count == 1)
            {
                UpdateIndex();
            }
            
            // Trigger the ItemAdded event
            ItemAdded?.Invoke(this, new ItemModifiedEventArgs<T>(item, position));
        }
        
        /// <summary>
        /// Removes a specific <typeparamref name="T" />.
        /// </summary>
        /// <param name="item">The <typeparamref name="T" /> to remove.</param>
        public void Remove(T item)
        {
            int position = items.IndexOf(item);
            if (position >= 0)
            {
                T removedItem = items[position];
                if (items.Remove(item))
                {
                    FixIndexIfRequired();
                    // Trigger the ItemRemoved event
                    ItemRemoved?.Invoke(this, new ItemModifiedEventArgs<T>(removedItem, position));
                }
            }
        }
        
        /// <summary>
        /// Removes a <typeparamref name="T" /> at a specific location.
        /// </summary>
        /// <param name="position">The position of the <typeparamref name="T" />.</param>
        public void RemoveAt(int position)
        {
            if (position >= items.Count)
            {
                return;
            }
            
            T removedItem = items[position];
            items.RemoveAt(position);
            FixIndexIfRequired();
            UpdateIndex();
            
            // Trigger the ItemRemoved event
            ItemRemoved?.Invoke(this, new ItemModifiedEventArgs<T>(removedItem, position));
        }
        
        /// <summary>
        /// Removes all of the items that match the <paramref name="pred"/>.
        /// </summary>
        /// <param name="pred">The function to use as a check.</param>
        public void Remove(Func<T, bool> pred)
        {
            if (items.RemoveAll(pred.Invoke) > 0)
            {
                FixIndexIfRequired();
            }
        }
        /// <summary>
        /// Removes all of the <typeparamref name="T" /> from this item.
        /// </summary>
        public void Clear()
        {
            items.Clear();

            UpdateIndex();
        }
        /// <summary>
        /// Recalculates the item positions and sizes with the specified values.
        /// </summary>
        /// <param name="pos">The position of the item.</param>
        /// <param name="size">The size of the item.</param>
        /// <param name="selected">If this item has been selected.</param>
        public override void Recalculate(PointF pos, SizeF size, bool selected)
        {
            base.Recalculate(pos, size, selected);

            text.Position = new PointF(pos.X + size.Width - RightArrow.Size.Width - 1 - text.Width, pos.Y + 3);
            LeftArrow.Position = new PointF(text.Position.X - LeftArrow.Size.Width, pos.Y + 4);
        }
        /// <summary>
        /// Moves to the previous item.
        /// </summary>
        public override void GoLeft()
        {
            if (Items.Count == 0)
            {
                return;
            }

            if (index == 0)
            {
                index = Items.Count - 1;
            }
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
            if (Items.Count == 0)
            {
                return;
            }

            if (index == Items.Count - 1)
            {
                index = 0;
            }
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
