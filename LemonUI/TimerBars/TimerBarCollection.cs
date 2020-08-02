#if FIVEM
using CitizenFX.Core.UI;
#elif SHVDN2
using GTA;
#elif SHVDN3
using GTA.UI;
#endif
using System;
using System.Collections.Generic;
using System.Drawing;

namespace LemonUI.TimerBars
{
    /// <summary>
    /// A collection or Set of <see cref="TimerBar"/>.
    /// </summary>
    public class TimerBarCollection : IContainer<TimerBar>
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
        /// Adds a <see cref="TimerBar"/> onto this collection.
        /// </summary>
        /// <param name="bar">The <see cref="TimerBar"/> to add.</param>
        public void Add(TimerBar bar)
        {
            // If the item is already on the list, raise an exception
            if (TimerBars.Contains(bar))
            {
                throw new InvalidOperationException("The item is already part of the menu.");
            }
            // Also raise an exception if is null
            if (bar == null)
            {
                throw new ArgumentNullException(nameof(bar));
            }
            // If we got here, add it
            TimerBars.Add(bar);
            // And recalculate the positions of the existing items
            Recalculate();
        }
        /// <summary>
        /// Removes a <see cref="TimerBar"/> from the Collection.
        /// </summary>
        /// <param name="bar">The <see cref="TimerBar"/> to remove.</param>
        public void Remove(TimerBar bar)
        {
            // If the bar is not present, return
            if (!TimerBars.Contains(bar))
            {
                return;
            }

            // Otherwise, remove it
            TimerBars.Remove(bar);
            // And recalculate the positions
            Recalculate();
        }
        /// <summary>
        /// Removes all of the <see cref="TimerBar"/> that match the function.
        /// </summary>
        /// <param name="func">The function to check the <see cref="TimerBar"/>.</param>
        public void Remove(Func<TimerBar, bool> func)
        {
            // Iterate over the timer bars
            foreach (TimerBar bar in new List<TimerBar>(TimerBars))
            {
                // If it matches the function, remove it
                if (func(bar))
                {
                    TimerBars.Remove(bar);
                }
            }
            // Finally, recalculate the positions
            Recalculate();
        }
        /// <summary>
        /// Removes all of the <see cref="TimerBar"/> in this collection.
        /// </summary>
        public void Clear() => TimerBars.Clear();
        /// <summary>
        /// Checks if the <see cref="TimerBar"/> is part of this collection.
        /// </summary>
        /// <param name="bar">The <see cref="TimerBar"/> to check.</param>
        public bool Contains(TimerBar bar) => TimerBars.Contains(bar);
        /// <summary>
        /// Recalculates the positions and sizes of the <see cref="TimerBar"/>.
        /// </summary>
        public void Recalculate()
        {
            // Get the position of 0,0 while staying safe zone aware
            Screen.SetElementAlignment(GFXAlignment.Right, GFXAlignment.Bottom);
            PointF pos = Screen.GetRealPosition(PointF.Empty);
            Screen.ResetElementAlignment();

            // Iterate over the existing timer bars and save the count
            int count = 0;
            foreach (TimerBar timerBar in TimerBars)
            {
                // And send them to the timer bar
                timerBar.Recalculate(new PointF(pos.X - TimerBar.backgroundWidth, pos.Y - (TimerBar.backgroundHeight * (TimerBars.Count - count)) - (TimerBar.separation * (TimerBars.Count - count - 1))));
                // Finish by increasing the total count of items
                count++;
            }
        }
        /// <summary>
        /// Draws the known timer bars.
        /// </summary>
        public void Process()
        {
            // If there are no timer bars or the collection is disabled, return
            if (TimerBars.Count == 0 || !Visible)
            {
                return;
            }

            // Hide the texts in the bottom right corner of the screen
#if FIVEM
            CitizenFX.Core.UI.Screen.Hud.HideComponentThisFrame(HudComponent.AreaName);
            CitizenFX.Core.UI.Screen.Hud.HideComponentThisFrame(HudComponent.StreetName);
            CitizenFX.Core.UI.Screen.Hud.HideComponentThisFrame(HudComponent.VehicleName);
#elif SHVDN2
            UI.HideHudComponentThisFrame(HudComponent.AreaName);
            UI.HideHudComponentThisFrame(HudComponent.StreetName);
            UI.HideHudComponentThisFrame(HudComponent.VehicleName);
#elif SHVDN3
            Hud.HideComponentThisFrame(HudComponent.AreaName);
            Hud.HideComponentThisFrame(HudComponent.StreetName);
            Hud.HideComponentThisFrame(HudComponent.VehicleName);
#endif
            // Draw the existing timer bars
            foreach (TimerBar timerBar in TimerBars)
            {
                timerBar.Draw();
            }
        }

        #endregion
    }
}
