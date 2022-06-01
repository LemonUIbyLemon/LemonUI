using System.Drawing;

namespace LemonUI.Menus
{
    /// <summary>
    /// Represents the Previous and Current X and Y values when changing the position on a grid.
    /// </summary>
    public class GridValueChangedArgs
    {
        /// <summary>
        /// The values present before they were changed.
        /// </summary>
        public PointF Before { get; }
        /// <summary>
        /// The values present after they were changed.
        /// </summary>
        public PointF After { get; }

        internal GridValueChangedArgs(PointF before, PointF after)
        {
            Before = before;
            After = after;
        }
    }
}
