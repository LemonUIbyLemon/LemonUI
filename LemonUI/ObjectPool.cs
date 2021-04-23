#if FIVEM
using CitizenFX.Core;
using CitizenFX.Core.UI;
using CitizenFX.Core.Native;
#elif RPH
using Rage;
using Rage.Native;
#elif SHVDN2
using GTA;
using GTA.Native;
#elif SHVDN3
using GTA.Native;
using GTA.UI;
#endif
using System;
using System.Collections.Generic;
using System.Drawing;

namespace LemonUI
{
    /// <summary>
    /// Represents the method that reports a Resolution change in the Game Settings.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="ResolutionChangedEventArgs"/> containing the Previous and Current resolution.</param>
    public delegate void ResolutionChangedEventHandler(object sender, ResolutionChangedEventArgs e);
    /// <summary>
    /// Represents the method that reports a Safe Zone change in the Game Settings.
    /// </summary>
    /// <param name="sender">The source of the event event.</param>
    /// <param name="e">A <see cref="ResolutionChangedEventArgs"/> containing the Previous and Current Safe Zone.</param>
    public delegate void SafeZoneChangedEventHandler(object sender, SafeZoneChangedEventArgs e);

    /// <summary>
    /// Represents the information after a Resolution Change in the game.
    /// </summary>
    public class ResolutionChangedEventArgs
    {
        /// <summary>
        /// The Game Resolution before it was changed.
        /// </summary>
        public SizeF Before { get; }
        /// <summary>
        /// The Game Resolution after it was changed.
        /// </summary>
        public SizeF After { get; }

        internal ResolutionChangedEventArgs(SizeF before, SizeF after)
        {
            Before = before;
            After = after;
        }
    }
    /// <summary>
    /// Represents the information after a Safe Zone Change in the game.
    /// </summary>
    public class SafeZoneChangedEventArgs
    {
        /// <summary>
        /// The raw Safezone size before the change.
        /// </summary>
        public float Before { get; }
        /// <summary>
        /// The Safezone size after the change.
        /// </summary>
        public float After { get; }

        internal SafeZoneChangedEventArgs(float before, float after)
        {
            Before = before;
            After = after;
        }
    }

    /// <summary>
    /// Manager for Menus and Items.
    /// </summary>
    public class ObjectPool
    {
        #region Private Fields

        /// <summary>
        /// The last known resolution by the object pool.
        /// </summary>
#if FIVEM
        private SizeF lastKnownResolution = CitizenFX.Core.UI.Screen.Resolution;
#elif RPH
        private SizeF lastKnownResolution = Game.Resolution;
#elif SHVDN2
        private SizeF lastKnownResolution = Game.ScreenResolution;
#elif SHVDN3
        private SizeF lastKnownResolution = GTA.UI.Screen.Resolution;
#endif
        /// <summary>
        /// The last know Safezone size.
        /// </summary>
#if FIVEM
        private float lastKnownSafezone = API.GetSafeZoneSize();
#elif RPH
        private float lastKnownSafezone = NativeFunction.CallByHash<float>(0xBAF107B6BB2C97F0);
#elif (SHVDN2 || SHVDN3)
        private float lastKnownSafezone = Function.Call<float>(Hash.GET_SAFE_ZONE_SIZE);
#endif
        /// <summary>
        /// The list of processable objects.
        /// </summary>
        private readonly List<IProcessable> objects = new List<IProcessable>();

        #endregion

        #region Public Properties

        /// <summary>
        /// Checks if there are objects visible on the screen.
        /// </summary>
        public bool AreAnyVisible
        {
            get
            {
                // Iterate over the objects
                foreach (IProcessable obj in objects)
                {
                    // If is visible return true
                    if (obj.Visible)
                    {
                        return true;
                    }
                }
                // If none were visible return false
                return false;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when the game resolution is changed.
        /// </summary>
        public event ResolutionChangedEventHandler ResolutionChanged;
        /// <summary>
        /// Event triggered when the Safezone size option in the Display settings is changed.
        /// </summary>
        public event SafeZoneChangedEventHandler SafezoneChanged;

        #endregion

        #region Tools

        /// <summary>
        /// Detects resolution changes by comparing the last known resolution and the current one.
        /// </summary>
        private void DetectResolutionChanges()
        {
            // Get the current resolution
#if FIVEM
            SizeF resolution = CitizenFX.Core.UI.Screen.Resolution;
#elif RPH
            SizeF resolution = Game.Resolution;
#elif SHVDN2
            SizeF resolution = Game.ScreenResolution;
#elif SHVDN3
            SizeF resolution = GTA.UI.Screen.Resolution;
#endif
            // If the old res does not matches the current one
            if (lastKnownResolution != resolution)
            {
                // Trigger the event
                ResolutionChanged?.Invoke(this, new ResolutionChangedEventArgs(lastKnownResolution, resolution));
                // Refresh everything
                RefreshAll();
                // And save the new resolution
                lastKnownResolution = resolution;
            }
        }
        /// <summary>
        /// Detects Safezone changes by comparing the last known value to the current one.
        /// </summary>
        private void DetectSafezoneChanges()
        {
            // Get the current Safezone size
#if FIVEM
            float safezone = API.GetSafeZoneSize();
#elif RPH
            float safezone = NativeFunction.CallByHash<float>(0xBAF107B6BB2C97F0);
#elif (SHVDN2 || SHVDN3)
            float safezone = Function.Call<float>(Hash.GET_SAFE_ZONE_SIZE);
#endif

            // If is not the same as the last one
            if (lastKnownSafezone != safezone)
            {
                // Trigger the event
                SafezoneChanged?.Invoke(this, new SafeZoneChangedEventArgs(lastKnownSafezone, safezone));
                // Refresh everything
                RefreshAll();
                // And save the new safezone
                lastKnownSafezone = safezone;
            }
        }

        #endregion

        #region Public Function

        /// <summary>
        /// Adds the object into the pool.
        /// </summary>
        /// <param name="obj">The object to add.</param>
        public void Add(IProcessable obj)
        {
            // Make sure that the object is not null
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            // Otherwise, add it to the general pool
            if (objects.Contains(obj))
            {
                throw new InvalidOperationException("The object is already part of this pool.");
            }
            objects.Add(obj);
        }
        /// <summary>
        /// Removes the object from the pool.
        /// </summary>
        /// <param name="obj">The object to remove.</param>
        public void Remove(IProcessable obj)
        {
            objects.Remove(obj);
        }
        /// <summary>
        /// Performs the specified action on each element that matches T.
        /// </summary>
        /// <typeparam name="T">The type to match.</typeparam>
        /// <param name="action">The action delegate to perform on each T.</param>
        public void ForEach<T>(Action<T> action)
        {
            foreach (IProcessable obj in objects)
            {
                if (obj is T conv)
                {
                    action(conv);
                }
            }
        }
        /// <summary>
        /// Refreshes all of the items.
        /// </summary>
        public void RefreshAll()
        {
            // Iterate over the objects and recalculate those possible
            foreach (IProcessable obj in objects)
            {
                if (obj is IRecalculable recal)
                {
                    recal.Recalculate();
                }
            }
        }
        /// <summary>
        /// Hides all of the objects.
        /// </summary>
        public void HideAll()
        {
            foreach (IProcessable obj in objects)
            {
                obj.Visible = false;
            }
        }
        /// <summary>
        /// Processes the objects and features in this pool.
        /// This needs to be called every tick.
        /// </summary>
        public void Process()
        {
            // See if there are resolution or safezone changes
            DetectResolutionChanges();
            DetectSafezoneChanges();
            // And process the objects in the pool
            foreach (IProcessable obj in objects)
            {
                obj.Process();
            }
        }

        #endregion
    }
}
