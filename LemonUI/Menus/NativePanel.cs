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
        /// If this panel is visible to the user.
        /// </summary>
        public virtual bool Visible { get; set; } = true;
        /// <summary>
        /// If the item has controls that can be clicked.
        /// </summary>
        public virtual bool Clickable { get; } = false;
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
