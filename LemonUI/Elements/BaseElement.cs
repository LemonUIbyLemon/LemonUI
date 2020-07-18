using LemonUI.Extensions;
using System.Drawing;

namespace LemonUI.Elements
{
    /// <summary>
    /// Base class for all of the 2D elements.
    /// </summary>
    public abstract class BaseElement : I2Dimensional
    {
        #region Private Fields

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
        /// The Color of the drawable.
        /// </summary>
        public Color Color { get; set; } = Color.FromArgb(255, 255, 255, 255);
        /// <summary>
        /// The rotation of the drawable.
        /// </summary>
        public float Heading { get; set; } = 0;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="BaseElement"/> with the specified Position and Size.
        /// </summary>
        /// <param name="pos">The position of the Element.</param>
        /// <param name="size">The size of the Element.</param>
        public BaseElement(PointF pos, SizeF size)
        {
            literalPosition = pos;
            literalSize = size;
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
