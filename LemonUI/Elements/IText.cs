#if FIVEM
using Alignment = CitizenFX.Core.UI.Alignment;
using Font = CitizenFX.Core.UI.Font;
#elif SHVDN2
using Font = GTA.Font;
#elif SHVDN3
using Alignment = GTA.UI.Alignment;
using Font = GTA.UI.Font;
#endif
using System.Drawing;

namespace LemonUI.Elements
{
    /// <summary>
    /// A Drawable screen text.
    /// </summary>
    public interface IText : IRecalculable, IDrawable
    {
        /// <summary>
        /// The position of the text.
        /// </summary>
        PointF Position { get; set; }
        /// <summary>
        /// The text to draw.
        /// </summary>
        string Text { get; set; }
        /// <summary>
        /// The color of the text.
        /// </summary>
        Color Color { get; set; }
        /// <summary>
        /// The game font to use.
        /// </summary>
        Font Font { get; set; }
        /// <summary>
        /// The scale of the text.
        /// </summary>
        float Scale { get; set; }
        /// <summary>
        /// If the text should have a drop down shadow.
        /// </summary>
        bool Shadow { get; set; }
        /// <summary>
        /// If the text should have an outline.
        /// </summary>
        bool Outline { get; set; }
        /// <summary>
        /// The alignment of the text.
        /// </summary>
        Alignment Alignment { get; set; }
        /// <summary>
        /// The maximum distance from X where the text would wrap into a new line.
        /// </summary>
        float WordWrap { get; set; }
        /// <summary>
        /// The width that the text takes from the screen.
        /// </summary>
        float Width { get; }
        /// <summary>
        /// The number of lines used by this text.
        /// </summary>
        int LineCount { get; }
        /// <summary>
        /// The height of each line of text.
        /// </summary>
        float LineHeight { get; }
    }
}
