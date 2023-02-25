using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace LemonUI.Menus
{
    /// <summary>
    /// Represents a Statistics panel.
    /// </summary>
    public class NativeStatsPanel : NativePanel, IContainer<NativeStatsInfo>, IEnumerable<NativeStatsInfo>
    {
        #region Fields

        private readonly List<NativeStatsInfo> fields = new List<NativeStatsInfo>();
        private PointF lastPosition = PointF.Empty;
        private float lastWidth = 0;
        private Color backgroundColor = Color.FromArgb(255, 169, 169, 169);
        private Color foregroundColor = Color.FromArgb(255, 255, 255, 255);

        #endregion

        #region Properties

        /// <summary>
        /// The color of the background of the bars.
        /// </summary>
        public Color BackgroundColor
        {
            get => backgroundColor;
            set
            {
                backgroundColor = value;

                foreach (NativeStatsInfo field in fields)
                {
                    field.SetColor(value, foregroundColor);
                }
            }
        }
        /// <summary>
        /// The color of the foreground of the bars.
        /// </summary>
        public Color ForegroundColor
        {
            get => foregroundColor;
            set
            {
                foregroundColor = value;

                foreach (NativeStatsInfo field in fields)
                {
                    field.SetColor(backgroundColor, value);
                }
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new Stats Panel.
        /// </summary>
        /// <param name="stats">The Statistics to add.</param>
        public NativeStatsPanel(params NativeStatsInfo[] stats)
        {
            foreach (NativeStatsInfo field in stats)
            {
                Add(field);
            }
        }

        #endregion

        #region Functions

        /// <inheritdoc/>
        public IEnumerator<NativeStatsInfo> GetEnumerator() => fields.GetEnumerator();
        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
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
            field.SetColor(backgroundColor, foregroundColor);
        }
        /// <summary>
        /// Removes a field from the panel.
        /// </summary>
        /// <param name="field">The field to remove.</param>
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

            const float differenceTop = 7;
            const float perStatSeparation = 38;

            Background.Size = new SizeF(width, differenceTop + (perStatSeparation * fields.Count));

            for (int i = 0; i < fields.Count; i++)
            {
                PointF fieldPosition = new PointF(position.X, position.Y + differenceTop + (perStatSeparation * i));
                fields[i].Recalculate(fieldPosition, width);
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
