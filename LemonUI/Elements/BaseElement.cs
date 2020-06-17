#if FIVEM
using CitizenFX.Core.UI;
#elif SHVDN2
using GTA;
#elif SHVDN3
using GTA.UI;
#endif
using LemonUI.Extensions;
using System;
using System.Drawing;

namespace LemonUI.Elements
{
    /// <summary>
    /// Base class for all of the 2D elements.
    /// </summary>
    public abstract class BaseElement : IScreenDrawable
    {
        #region Private Fields

        /// <summary>
        /// The alignment of the item.
        /// </summary>
        protected internal Alignment alignment = Alignment.Left;
        /// <summary>
        /// The 1080 scaled position.
        /// </summary>
        protected internal PointF literalPosition = PointF.Empty;
        /// <summary>
        /// The relative position between 0 and 1.
        /// </summary>
        protected internal PointF relativePosition = PointF.Empty;
        /// <summary>
        /// The 1080 scaled size.
        /// </summary>
        protected internal SizeF literalSize = SizeF.Empty;
        /// <summary>
        /// The relative size between 0 and 1.
        /// </summary>
        protected internal SizeF relativeSize = SizeF.Empty;

        #endregion

        #region Public Properties

        /// <summary>
        /// The Position of the drawable.
        /// </summary>
        public PointF Position
        {
            get
            {
                return literalPosition;
            }
            set
            {
                literalPosition = value;
                Recalculate();
            }
        }
        /// <summary>
        /// The Size of the drawable.
        /// </summary>
        public SizeF Size
        {
            get
            {
                return literalSize;
            }
            set
            {
                literalSize = value;
                Recalculate();
            }
        }
        /// <summary>
        /// The alignment of the element.
        /// </summary>
        public virtual Alignment Alignment
        {
            get => alignment;
            set
            {
                if (value != Alignment.Left && value != Alignment.Right)
                {
                    throw new InvalidOperationException("This Element can't be centered.");
                }

                alignment = value;
                Recalculate();
            }
        }
        /// <summary>
        /// The Color of the drawable.
        /// </summary>
        public Color Color { get; set; } = Color.FromArgb(255, 255, 255, 255);
        /// <summary>
        /// The rotation of the drawable.
        /// </summary>
        public float Heading { get; set; } = 0;

        #endregion

        #region Constructors

        public BaseElement(PointF pos, SizeF size)
        {
            // Save the position and size
            literalPosition = pos;
            literalSize = size;
            // And recalculate the relative values
            Recalculate();
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// Recalculates the size and position of this item.
        /// </summary>
        public virtual void Recalculate()
        {
            relativePosition = literalPosition.ToRelative();
            relativeSize = literalSize.ToRelative();
            if (alignment == Alignment.Right)
            {
                relativePosition.X = 1 - relativePosition.X - relativeSize.Width;
            }
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Draws the item on the screen.
        /// </summary>
        public abstract void Draw();

        #endregion
    }
}
