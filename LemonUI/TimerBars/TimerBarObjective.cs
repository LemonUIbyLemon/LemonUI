using System;
using System.Collections.Generic;
using System.Drawing;
using LemonUI.Elements;

namespace LemonUI.TimerBars
{
    /// <summary>
    /// A timer bar for a specific amount of objectives.
    /// </summary>
    public class TimerBarObjective : TimerBar
    {
        #region Fields

        private const float width = 20;
        private const float height = 20;

        private static readonly Color colorWhite = Color.FromArgb(255, 255, 255);
        private static readonly Color colorCompleted = Color.FromArgb(101, 180, 212);

        private readonly List<ScaledTexture> objectives = new List<ScaledTexture>();

        private PointF lastPosition = default;

        private int count = 1;
        private int completed = 0;
        private Color colorSet = colorCompleted;
        private ObjectiveSpacing objectiveSpacing = ObjectiveSpacing.Equal;

        #endregion

        #region Properties

        /// <summary>
        /// The number of objectives shown in the timer bar.
        /// </summary>
        public int Count
        {
            get => count;
            set
            {
                if (count == value)
                {
                    return;
                }

                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The number of objectives can't be under zero.");
                }

                count = value;
                UpdateObjectiveCount();
            }
        }
        /// <summary>
        /// The number of completed objectives.
        /// </summary>
        public int Completed
        {
            get => completed;
            set
            {
                if (completed == value)
                {
                    return;
                }

                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The number of completed objectives can't be under zero.");
                }
                if (value > Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The number of completed objectives can't be over the total number of objectives.");
                }

                completed = value;

                UpdateObjectiveColors();
            }
        }
        /// <summary>
        /// The color used for completed objectives.
        /// </summary>
        public Color CompletedColor
        {
            get => colorSet;
            set
            {
                if (colorSet == value)
                {
                    return;
                }

                colorSet = value;
                UpdateObjectiveColors();
            }
        }
        /// <summary>
        /// The type of spacing between the objectives .
        /// </summary>
        public ObjectiveSpacing Spacing
        {
            get => objectiveSpacing;
            set
            {
                if (objectiveSpacing == value)
                {
                    return;
                }

                objectiveSpacing = value;
                Recalculate(lastPosition);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new timer bar used to show objectives.
        /// </summary>
        /// <param name="title">The title of the objective bar.</param>
        public TimerBarObjective(string title) : base(title, string.Empty)
        {
            UpdateObjectiveCount();
        }

        #endregion

        #region Tools

        private void UpdateObjectiveCount()
        {
            // just to make sure
            if (completed > count)
            {
                completed = count;
            }

            objectives.Clear();

            for (int i = 0; i < count; i++)
            {
                objectives.Add(new ScaledTexture("timerbars", "circle_checkpoints"));
            }

            UpdateObjectiveColors();
        }
        private void UpdateObjectiveColors()
        {
            for (int i = 0; i < objectives.Count; i++)
            {
                ScaledTexture texture = objectives[i];
                texture.Color = i < completed ? colorSet : colorWhite;
            }
        }

        #endregion

        #region Functions

        /// <summary>
        /// Draws the objective timer bar.
        /// </summary>
        public override void Draw()
        {
            background.Draw();
            title.Draw();

            foreach (ScaledTexture texture in objectives)
            {
                texture.Draw();
            }
        }
        /// <inheritdoc/>
        public override void Recalculate(PointF pos)
        {
            lastPosition = pos;

            base.Recalculate(pos);

            const float safe = width + 5;
            float startY = pos.Y + (backgroundHeight * 0.5f) - (height * 0.5f);

            switch (objectiveSpacing)
            {
                case ObjectiveSpacing.Equal:
                {
                    const float half = backgroundWidth * 0.5f;
                    float startX = pos.X + half;
                    float spacingWidth = (half - safe) / (objectives.Count - 1);

                    for (int i = 0; i < objectives.Count; i++)
                    {
                        ScaledTexture texture = objectives[i];
                        texture.Size = new SizeF(width, height);
                        texture.Position = new PointF(startX + (spacingWidth * i), startY);
                    }

                    break;
                }
                case ObjectiveSpacing.Fixed:
                {
                    float startX = pos.X + backgroundWidth - safe - (width * (count - 1));

                    for (int i = 0; i < objectives.Count; i++)
                    {
                        ScaledTexture texture = objectives[i];
                        texture.Size = new SizeF(width, height);
                        texture.Position = new PointF(startX + (i * width), startY);
                    }

                    break;
                }
            }
        }

        #endregion
    }
}
