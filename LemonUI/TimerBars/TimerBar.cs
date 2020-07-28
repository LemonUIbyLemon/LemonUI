using LemonUI.Elements;
using System.Drawing;
using System.Reflection;

namespace LemonUI.TimerBars
{
    /// <summary>
    /// A Bar with information shown in the bottom right corner of the screen.
    /// </summary>
    public class TimerBar : IDrawable
    {
        #region Constant Fields

        /// <summary>
        /// The separation between the different timer bars.
        /// </summary>
        internal const float separation = 4;
        /// <summary>
        /// The width of the background.
        /// </summary>
        internal const float backgroundWidth = 220;
        /// <summary>
        /// The height of the background.
        /// </summary>
        internal const float backgroundHeight = 37;

        #endregion

        #region Internal Fields

        /// <summary>
        /// The background of the timer bar.
        /// </summary>
        internal protected readonly ScaledTexture background = new ScaledTexture("timerbars", "all_black_bg")
        {
            Color = Color.FromArgb(160, 255, 255, 255)
        };

        #endregion

        #region Public Properties

        /// <summary>
        /// The title of the bar, shown on the left.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The information shown on the right.
        /// </summary>
        public string Info { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="TimerBar"/> with the specified Title and Value.
        /// </summary>
        /// <param name="title">The title of the bar.</param>
        /// <param name="info">The information shown on the bar.</param>
        public TimerBar(string title, string info)
        {
            Title = title;
            Info = info;
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Recalculates the position of the timer bar elements based on the location of it on the screen.
        /// </summary>
        /// <param name="pos">The position of the item.</param>
        public virtual void Recalculate(PointF pos)
        {
            // Set the size of the background
            background.Position = pos;
            background.Size = new SizeF(backgroundWidth, backgroundHeight);
        }
        /// <summary>
        /// Draws the timer bar information.
        /// </summary>
        public void Draw()
        {
            background.Draw();
        }

        #endregion
    }
}
