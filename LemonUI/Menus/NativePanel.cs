using LemonUI.Elements;
using System.Drawing;

namespace LemonUI.Menus
{
    /// <summary>
    /// Represents a panel shown under the description of the item description.
    /// </summary>
    public abstract class NativePanel
    {
        #region Public Properties

        /// <summary>
        /// The Background of the panel itself.
        /// </summary>
        public ScaledTexture Background { get; } = new ScaledTexture("commonmenu", "gradient_bgd");

        #endregion

        #region Public Functions

        /// <summary>
        /// Recalculates the menu contents.
        /// </summary>
        /// <param name="position">The position of the panel.</param>
        /// <param name="width">The width of the menu.</param>
        public virtual void Recalculate(PointF position, float width)
        {
            Background.Position = position;
        }
        /// <summary>
        /// Processes and Draws the panel.
        /// </summary>
        public virtual void Process()
        {
            Background.Draw();
        }

        #endregion
    }
}
