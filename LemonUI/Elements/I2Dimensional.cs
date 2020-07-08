using System.Drawing;

namespace LemonUI.Elements
{
    /// <summary>
    /// A 2D item that can be drawn on the screen.
    /// </summary>
    public interface I2Dimensional : IRecalculable, IDrawable
    {
        /// <summary>
        /// The Position of the drawable.
        /// </summary>
        PointF Position { get; set; }
        /// <summary>
        /// The Size of the drawable.
        /// </summary>
        SizeF Size { get; set; }
        /// <summary>
        /// The Color of the drawable.
        /// </summary>
        Color Color { get; set; }
    }
}
