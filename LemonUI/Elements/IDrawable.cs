#if FIVEM
using CitizenFX.Core.UI;
#elif SHVDN3
using GTA.UI;
#endif
using System.Drawing;

namespace LemonUI.Elements
{
    /// <summary>
    /// A 2D item that can be drawn on the screen.
    /// </summary>
    public interface IScreenDrawable : IRecalculable, IDrawable
    {
        #region Public Properties

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
        /// <summary>
        /// The rotation of the drawable.
        /// </summary>
        float Heading { get; set; }
        /// <summary>
        /// The alignment of the element.
        /// </summary>
        Alignment Alignment { get; set; }

        #endregion
    }
}
