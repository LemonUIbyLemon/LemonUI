using LemonUI.Elements;
using System;
using System.Drawing;

namespace LemonUI.Menus
{
    /// <summary>
    /// A slider item for changing integer values.
    /// </summary>
    public class NativeSliderItem : NativeSlidableItem
    {
        #region Internal Fields

        /// <summary>
        /// The background of the slider.
        /// </summary>
        internal protected ScaledRectangle background = new ScaledRectangle(PointF.Empty, SizeF.Empty)
        {
            Color = Color.FromArgb(255, 4, 32, 57)
        };
        /// <summary>
        /// THe front of the slider.
        /// </summary>
        internal protected ScaledRectangle slider = new ScaledRectangle(PointF.Empty, SizeF.Empty)
        {
            Color = Color.FromArgb(255, 57, 116, 200)
        };

        #endregion

        #region Private Fields

        /// <summary>
        /// The maximum value of the slider.
        /// </summary>
        private int maximum = 0;
        /// <summary>
        /// The current value of the slider.
        /// </summary>
        private int _value = 100;

        #endregion

        #region Public Properties

        /// <summary>
        /// The maximum value of the slider.
        /// </summary>
        public int Maximum
        {
            get => maximum;
            set
            {
                // If the value was not changed, return
                if (maximum == value)
                {
                    return;
                }
                // Otherwise, save the new value
                maximum = value;
                // If the current value is higher than the max, set the max
                if (_value > maximum)
                {
                    _value = maximum;
                    ValueChanged?.Invoke(this, EventArgs.Empty);
                }
                // Finally, update the location of the slider
                UpdatePosition();
            }
        }
        /// <summary>
        /// The current value of the slider.
        /// </summary>
        public int Value
        {
            get => _value;
            set
            {
                // If the value is over the limit, raise an exception
                if (value > maximum)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"The value is over the maximum of {maximum - 1}");
                }
                // Otherwise, save it
                _value = value;
                // Trigger the respective event
                ValueChanged?.Invoke(this, EventArgs.Empty);
                // And update the location of the slider
                UpdatePosition();
            }
        }
        /// <summary>
        /// The multiplier for increasing and decreasing the value.
        /// </summary>
        public int Multiplier { get; set; } = 1;

        #endregion

        #region Event

        /// <summary>
        /// Event triggered when the value of the menu changes.
        /// </summary>
        public event EventHandler ValueChanged;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a <see cref="NativeSliderItem"/> with a maximum of 100.
        /// </summary>
        /// <param name="title">The title of the Item.</param>
        public NativeSliderItem(string title) : this(title, "", 100, 0)
        {
        }
        /// <summary>
        /// Creates a <see cref="NativeSliderItem"/> with a maximum of 100.
        /// </summary>
        /// <param name="title">The title of the Item.</param>
        /// <param name="description">The description of the Item.</param>
        public NativeSliderItem(string title, string description) : this(title, description, 100, 0)
        {
        }
        /// <summary>
        /// Creates a <see cref="NativeSliderItem"/> with a specific current and maximum value.
        /// </summary>
        /// <param name="title">The title of the Item.</param>
        /// <param name="max">The maximum value of the Slider.</param>
        /// <param name="value">The current value of the Slider.</param>
        public NativeSliderItem(string title, int max, int value) : this(title, "", max, value)
        {
        }
        /// <summary>
        /// Creates a <see cref="NativeSliderItem"/> with a specific maximum.
        /// </summary>
        /// <param name="title">The title of the Item.</param>
        /// <param name="description">The description of the Item.</param>
        /// <param name="max">The maximum value of the Slider.</param>
        /// <param name="value">The current value of the Slider.</param>
        public NativeSliderItem(string title, string description, int max, int value) : base(title, description)
        {
            maximum = max;
            _value = value;
        }

        #endregion

        #region Internal Functions

        /// <summary>
        /// Updates the position of the bar based on the value.
        /// </summary>
        internal protected void UpdatePosition()
        {
            // Calculate the increment, and then the value of X
            float increment = _value / (float)maximum;
            float x = (background.Size.Width - slider.Size.Width) * increment;
            // Then, add the X to the slider position
            slider.Position = new PointF(background.Position.X + x, background.Position.Y);
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
            // Set the position and size of the background
            background.Size = new SizeF(150, 9);
            background.Position = new PointF(pos.X + size.Width - background.Size.Width - 7 - LeftArrow.Size.Width, pos.Y + 14);
            // And do the same for the left arrow
            LeftArrow.Position = new PointF(background.Position.X - LeftArrow.Size.Width, pos.Y + 4);
            // Finally, set the correct position of the slider
            slider.Size = new SizeF(75, 9);
            UpdatePosition();
        }
        /// <summary>
        /// Reduces the value of the slider.
        /// </summary>
        public override void GoLeft()
        {
            // Calculate the new value
            int newValue = _value - Multiplier;
            // If is under zero, set it to zero
            if (newValue < 0)
            {
                Value = 0;
            }
            // Otherwise, set it to the new value
            else
            {
                Value = newValue;
            }
        }
        /// <summary>
        /// Increases the value of the slider.
        /// </summary>
        public override void GoRight()
        {
            // Calculate the new value
            int newValue = _value + Multiplier;
            // If the value is over the maximum, set the max
            if (newValue > maximum)
            {
                Value = maximum;
            }
            // Otherwise, set the calculated value
            else
            {
                Value = newValue;
            }
        }
        /// <summary>
        /// Draws the slider.
        /// </summary>
        public override void Draw()
        {
            base.Draw(); // Arrows, Title and Left Badge
            background.Draw();
            slider.Draw();
        }

        #endregion
    }
}
