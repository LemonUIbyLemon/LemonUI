#if FIVEM
using CitizenFX.Core.Native;
#elif SHVDN2
using GTA.Native;
#elif SHVDN3
using GTA.Native;
#endif
using LemonUI.Extensions;
using System.Collections.Generic;
using System.Drawing;

namespace LemonUI.TimerBars
{
    /// <summary>
    /// A collection or Set of <see cref="TimerBar"/>.
    /// </summary>
    public class TimerBarCollection : IRecalculable, IProcessable
    {
        #region Public Properties

        /// <summary>
        /// If this collection of Timer Bars is visible to the user.
        /// </summary>
        public bool Visible { get; set; } = true;
        /// <summary>
        /// The <see cref="TimerBar"/>s that are part of this collection.
        /// </summary>
        public List<TimerBar> TimerBars { get; } = new List<TimerBar>();

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new collection of Timer Bars.
        /// </summary>
        /// <param name="bars"></param>
        public TimerBarCollection(params TimerBar[] bars)
        {
            TimerBars.AddRange(bars);
            Recalculate();
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Recalculates the positions and sizes of the Timer Bars.
        /// </summary>
        public void Recalculate()
        {
            // Iterate over the existing timer bars and save the count
            int count = 0;
            foreach (TimerBar timerBar in TimerBars)
            {
                // Calculate the Y position
                float y = (TimerBar.backgroundHeight + TimerBar.separation) * count;
                // And send them to the timer bar
                timerBar.Recalculate(new PointF(0, -y));
                // Finish by increasing the total count of items
                count++;
            }
        }
        /// <summary>
        /// Draws the known timer bars.
        /// </summary>
        public void Process()
        {
            // Set the correct alignment
            Screen.SetElementAlignment(GFXAlignment.Right, GFXAlignment.Bottom);
            // Draw the existing timer bars
            foreach (TimerBar timerBar in TimerBars)
            {
                timerBar.Draw();
            }
            // And reset the custom alignment
            Screen.ResetElementAlignment();
        }

        #endregion
    }
}
