using System.Drawing;
using LemonUI.Elements;

namespace LemonUI.Menus
{
    /// <summary>
    /// Represents the Color Information shown on the Panel.
    /// </summary>
    public class NativeColorData
    {
        #region Fields

        internal readonly ScaledRectangle rectangle = new ScaledRectangle(PointF.Empty, SizeF.Empty);

        #endregion

        #region Properties

        /// <summary>
        /// The name of the color.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The RGBA values of the color.
        /// </summary>
        public Color Color
        {
            get => rectangle.Color;
            set => rectangle.Color = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new Color Panel information.
        /// </summary>
        /// <param name="name">The name of the color.</param>
        /// <param name="color">The RGBA values of the color.</param>
        public NativeColorData(string name, Color color)
        {
            Name = name;
            rectangle.Color = color;
        }

        #endregion
    }
}
