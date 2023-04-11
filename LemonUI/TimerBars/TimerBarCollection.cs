#if FIVEM
using CitizenFX.Core.UI;
#elif RAGEMP
using RAGE.Game;
#elif RPH
using Rage;
using Rage.Native;
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
        #region Fields

        private PointF offset = PointF.Empty;
        private float width = 250;

        #endregion

        #region Properties

        /// <summary>
        /// If this collection of Timer Bars is visible to the user.
        /// </summary>
        public bool Visible { get; set; } = true;
        /// <summary>
        /// The <see cref="TimerBar"/>s that are part of this collection.
        /// </summary>
        public List<TimerBar> TimerBars { get; } = new List<TimerBar>();
        /// <summary>
        /// The offset from it's starting point on the bottom right.
        /// </summary>
        public PointF Offset
        {
            get => offset;
            set
            {
                offset = value;
                Recalculate();
            }
        }
        /// <summary>
        /// Width of the <see cref="TimerBarCollection"/>.
        /// </summary>
        public float Width
        {
            get => width;
            set
            {
                if (value > 500)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Width of Timer Bar collection can't be larger than 500.");
                }
                if (value < 100)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Width of Timer Bar collection can't be less than 100.");
                }

                width = value;
                TimerBar.backgroundWidth = value;
                Recalculate();
            }
        }

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

        #region Functions

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
            for (int i = 0; i < TimerBars.Count; i++)
            {
                TimerBar bar = TimerBars[i];

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
            Screen.SetElementAlignment(GFXAlignment.Right, GFXAlignment.Bottom);
            PointF pos = Screen.GetRealPosition(PointF.Empty);
            Screen.ResetElementAlignment();

            pos.X += offset.X;
            pos.Y += offset.Y;

            int count = 0;

            for (int i = 0; i < TimerBars.Count; i++)
            {
                TimerBars[i].Recalculate(new PointF(pos.X - TimerBar.backgroundWidth, pos.Y - (TimerBar.backgroundHeight * (TimerBars.Count - count)) - (TimerBar.separation * (TimerBars.Count - count - 1))));
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
#elif RAGEMP
            Invoker.Invoke(Natives.HideHudComponentThisFrame, HudComponent.AreaName);
            Invoker.Invoke(Natives.HideHudComponentThisFrame, HudComponent.StreetName);
            Invoker.Invoke(Natives.HideHudComponentThisFrame, HudComponent.VehicleName);
#elif RPH
            NativeFunction.CallByHash<int>(0x6806C51AD12B83B8, 7);
            NativeFunction.CallByHash<int>(0x6806C51AD12B83B8, 9);
            NativeFunction.CallByHash<int>(0x6806C51AD12B83B8, 6);
#elif SHVDN3
            Hud.HideComponentThisFrame(HudComponent.AreaName);
            Hud.HideComponentThisFrame(HudComponent.StreetName);
            Hud.HideComponentThisFrame(HudComponent.VehicleName);
#endif
            for (int i = 0; i < TimerBars.Count; i++)
            {
                TimerBars[i].Draw();
            }
        }

        #endregion
    }
}
