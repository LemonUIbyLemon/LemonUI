using System.Drawing;

namespace LemonUI.Elements
{
    /// <summary>
    /// Defines the shadow style for a <see cref="LemonUI.Elements.ScaledText"/>.
    /// </summary>
    public class Shadow
    {
        /// <summary>
        /// The color used for the shadow.
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// The distance of the shadow.
        /// </summary>
        public int Distance { get; set; }
        /// <summary>
        /// Whether the shadow should use the classic non configurable style.
        /// </summary>
        public bool UseClassic { get; set; }
    }
}
