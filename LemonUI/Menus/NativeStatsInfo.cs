using System;
using System.Collections.Generic;
using System.Drawing;
using LemonUI.Elements;

namespace LemonUI.Menus
{
    /// <summary>
    /// Represents the Information of a specific field in a <see cref="NativeStatsPanel"/>.
    /// </summary>
    public class NativeStatsInfo
    {
        #region Fields

        private const float barWidth = 33;
        private const float barHeight = 9;
        private readonly ScaledText text = new ScaledText(PointF.Empty, string.Empty, 0.35f);
        private float value = 100;
        private readonly List<ScaledRectangle> backgrounds = new List<ScaledRectangle>();
        private readonly List<ScaledRectangle> foregrounds = new List<ScaledRectangle>();

        #endregion

        #region Properties

        /// <summary>
        /// The name of the Stats Field.
        /// </summary>
        public string Name
        {
            get => text.Text;
            set => text.Text = value ?? throw new ArgumentNullException(nameof(value));
        }
        /// <summary>
        /// The value of the Stats bar.
        /// </summary>
        public float Value
        {
            get => value;
            set
            {
                if (value > 100 || value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The Value of the Stat can't be over 100 or under 0.");
                }

                this.value = value;
                UpdateBars();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new Stat Info with the specified name and value set to zero.
        /// </summary>
        /// <param name="name">The name of the Stat.</param>
        public NativeStatsInfo(string name) : this(name, 0)
        {
        }
        /// <summary>
        /// Creates a new Stat Info with the specified name and value.
        /// </summary>
        /// <param name="name">The name of the Stat.</param>
        /// <param name="value"></param>
        public NativeStatsInfo(string name, int value)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            this.value = value;

            for (int i = 0; i < 5; i++)
            {
                backgrounds.Add(new ScaledRectangle(PointF.Empty, SizeF.Empty));
                foregrounds.Add(new ScaledRectangle(PointF.Empty, SizeF.Empty));
            }
        }

        #endregion

        #region Functions

        internal void SetColor(Color background, Color foreground)
        {
            foreach (ScaledRectangle rectangle in backgrounds)
            {
                rectangle.Color = background;
            }
            foreach (ScaledRectangle rectangle in foregrounds)
            {
                rectangle.Color = foreground;
            }
        }
        /// <summary>
        /// Updates the values of the bars.
        /// </summary>
        private void UpdateBars()
        {
            SizeF @default = new SizeF(barWidth, barHeight);

            // FIRST BAR
            if (value > 0 && value < 20)
            {
                foregrounds[0].Size = new SizeF(barWidth * (value / 20), barHeight);
            }
            else
            {
                foregrounds[0].Size = value > 20 ? @default : SizeF.Empty;
            }

            // SECOND BAR
            if (value > 20 && value < 40)
            {
                foregrounds[1].Size = new SizeF(barWidth * ((value - 20) / 20), barHeight);
            }
            else
            {
                foregrounds[1].Size = value > 40 ? @default : SizeF.Empty;
            }

            // THIRD BAR
            if (value > 40 && value < 60)
            {
                foregrounds[2].Size = new SizeF(barWidth * ((value - 40) / 20), barHeight);
            }
            else
            {
                foregrounds[2].Size = value > 60 ? @default : SizeF.Empty;
            }

            // FOURTH BAR
            if (value > 60 && value < 80)
            {
                foregrounds[3].Size = new SizeF(barWidth * ((value - 60) / 20), barHeight);
            }
            else
            {
                foregrounds[3].Size = value > 80 ? @default : SizeF.Empty;
            }

            // FIFTH BAR
            if (value > 80 && value < 100)
            {
                foregrounds[4].Size = new SizeF(barWidth * ((value - 80) / 20), barHeight);
            }
            else
            {
                foregrounds[4].Size = value == 100 ? @default : SizeF.Empty;
            }
        }
        /// <summary>
        /// Recalculates the position of the stat Text and Bar.
        /// </summary>
        /// <param name="position">The new position fot the Stat.</param>
        /// <param name="width">The Width of the parent Stats Panel.</param>
        public void Recalculate(PointF position, float width)
        {
            const float barOffsetTop = 11;
            const float offsetLeft = 9;
            const float separatorSize = 4;
            const float rightMargin = 42;

            text.Position = new PointF(position.X + offsetLeft, position.Y);

            for (int i = 0; i < 5; i++)
            {
                PointF pos = new PointF(position.X + width - rightMargin - (barWidth * (5 - i)) - (separatorSize * (5 - i)), position.Y + barOffsetTop);

                backgrounds[i].Position = pos;
                backgrounds[i].Size = new SizeF(barWidth, barHeight);
                foregrounds[i].Position = pos;
            }

            UpdateBars();
        }
        /// <summary>
        /// Draws the stat information.
        /// </summary>
        public void Draw()
        {
            foreach (ScaledRectangle background in backgrounds)
            {
                background.Draw();
            }
            foreach (ScaledRectangle foreground in foregrounds)
            {
                foreground.Draw();
            }
            text.Draw();
        }

        #endregion
    }
}
