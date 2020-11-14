using LemonUI.Elements;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace LemonUI.Menus
{
    /// <summary>
    /// Represents the Information of a specific field in a <see cref="NativeStatsPanel"/>.
    /// </summary>
    public class NativeStatsInfo
    {
        #region Fields

        private readonly ScaledText text = new ScaledText(PointF.Empty, "", 0.35f);
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
            set => text.Text = value;
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

        #region Constructor

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
            Name = name;
            this.value = value;

            for (int i = 0; i < 5; i++)
            {
                backgrounds.Add(new ScaledRectangle(PointF.Empty, SizeF.Empty)
                {
                    Color = Color.FromArgb(255, 169, 169, 169)
                });
                foregrounds.Add(new ScaledRectangle(PointF.Empty, SizeF.Empty)
                {
                    Color = Color.FromArgb(255, 255, 255, 255)
                });
            }
        }

        #endregion

        #region Functions

        /// <summary>
        /// Updates the values of the bars.
        /// </summary>
        private void UpdateBars()
        {
            SizeF @default = new SizeF(35, 9);

            // FIRST BAR
            if (value > 0 && value < 20)
            {
                foregrounds[0].Size = new SizeF(@default.Width * (value / 20), @default.Height);
            }
            else
            {
                foregrounds[0].Size = value > 20 ? @default : SizeF.Empty;
            }

            // SECOND BAR
            if (value > 20 && value < 40)
            {
                foregrounds[1].Size = new SizeF(@default.Width * ((value - 20) / 20), @default.Height);
            }
            else
            {
                foregrounds[1].Size = value > 40 ? @default : SizeF.Empty;
            }

            // THIRD BAR
            if (value > 40 && value < 60)
            {
                foregrounds[2].Size = new SizeF(@default.Width * ((value - 40) / 20), @default.Height);
            }
            else
            {
                foregrounds[2].Size = value > 60 ? @default : SizeF.Empty;
            }

            // FOURTH BAR
            if (value > 60 && value < 80)
            {
                foregrounds[3].Size = new SizeF(@default.Width * ((value - 60) / 20), @default.Height);
            }
            else
            {
                foregrounds[3].Size = value > 80 ? @default : SizeF.Empty;
            }

            // FIFTH BAR
            if (value > 80 && value < 100)
            {
                foregrounds[4].Size = new SizeF(@default.Width * ((value - 80) / 20), @default.Height);
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
            text.Position = new PointF(position.X, position.Y);

            for (int i = 0; i < 5; i++)
            {
                PointF pos = new PointF(position.X + width - 234 + ((35 + 3) * i), position.Y + 10);
                backgrounds[i].Position = pos;
                backgrounds[i].Size = new SizeF(35, 9);
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

    /// <summary>
    /// Represents a Statistics panel.
    /// </summary>
    public class NativeStatsPanel : NativePanel, IContainer<NativeStatsInfo>
    {
        #region Fields

        private readonly List<NativeStatsInfo> fields = new List<NativeStatsInfo>();
        private PointF lastPosition = PointF.Empty;
        private float lastWidth = 0;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new Stats Panel.
        /// </summary>
        /// <param name="stats">The Statistics to add.</param>
        public NativeStatsPanel(params NativeStatsInfo[] stats)
        {
            fields.AddRange(stats);
        }

        #endregion

        #region Functions

        /// <summary>
        /// Adds a stat to the player field.
        /// </summary>
        /// <param name="field">The Field to add.</param>
        public void Add(NativeStatsInfo field)
        {
            if (fields.Contains(field))
            {
                throw new InvalidOperationException("Stat is already part of the panel.");
            }
            fields.Add(field);
        }
        /// <summary>
        /// Removes a field from the 
        /// </summary>
        /// <param name="field"></param>
        public void Remove(NativeStatsInfo field)
        {
            if (!fields.Contains(field))
            {
                return;
            }
            fields.Remove(field);
            Recalculate();
        }
        /// <summary>
        /// Removes the items that match the function.
        /// </summary>
        /// <param name="func">The function used to match items.</param>
        public void Remove(Func<NativeStatsInfo, bool> func)
        {
            foreach (NativeStatsInfo item in new List<NativeStatsInfo>(fields))
            {
                if (func(item))
                {
                    Remove(item);
                }
            }
        }
        /// <summary>
        /// Removes all of the Stats fields.
        /// </summary>
        public void Clear()
        {
            fields.Clear();
            Recalculate();
        }
        /// <summary>
        /// Checks if the field is part of the Stats Panel.
        /// </summary>
        /// <param name="field">The field to check.</param>
        /// <returns><see langword="true"/> if the item is part of the Panel, <see langword="false"/> otherwise.</returns>
        public bool Contains(NativeStatsInfo field) => fields.Contains(field);
        /// <summary>
        /// Recalculates the Stats panel with the last known Position and Width.
        /// </summary>
        public void Recalculate() => Recalculate(lastPosition, lastWidth);
        /// <summary>
        /// Recalculates the position of the Stats panel.
        /// </summary>
        /// <param name="position">The new position of the Stats Panel.</param>
        /// <param name="width">The width of the menu.</param>
        public override void Recalculate(PointF position, float width)
        {
            base.Recalculate(position, width);

            Background.Size = new SizeF(width, (fields.Count * 38) + 9);

            for (int i = 0; i < fields.Count; i++)
            {
                fields[i].Recalculate(new PointF(position.X + 9, position.Y + 9 + (38 * i)), width);
            }
        }
        /// <summary>
        /// Processes the Stats Panel.
        /// </summary>
        public override void Process()
        {
            base.Process();

            foreach (NativeStatsInfo info in fields)
            {
                info.Draw();
            }
        }

        #endregion
    }
}
