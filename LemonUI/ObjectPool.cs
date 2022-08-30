#if FIVEM
using CitizenFX.Core;
using CitizenFX.Core.UI;
using CitizenFX.Core.Native;
#elif RAGEMP
using RAGE.Game;
using RAGE.NUI;
#elif RPH
using Rage;
using Rage.Native;
#elif SHVDN3
using GTA.Native;
#endif
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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
#elif RAGEMP
        private SizeF lastKnownResolution = new SizeF(Game.ScreenResolution.Width, Game.ScreenResolution.Height);
#elif RPH
        private SizeF lastKnownResolution = Game.Resolution;
#elif SHVDN3
        private SizeF lastKnownResolution = GTA.UI.Screen.Resolution;
#endif
        /// <summary>
        /// The last know Safezone size.
        /// </summary>
#if FIVEM
        private float lastKnownSafezone = API.GetSafeZoneSize();
#elif RAGEMP
        private float lastKnownSafezone = Invoker.Invoke<float>(Natives.GetSafeZoneSize);
#elif RPH
        private float lastKnownSafezone = NativeFunction.CallByHash<float>(0xBAF107B6BB2C97F0);
#elif SHVDN3
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
#if FIVEM
            SizeF resolution = CitizenFX.Core.UI.Screen.Resolution;
#elif RAGEMP
            ScreenResolutionType raw = Game.ScreenResolution;
            SizeF resolution = new SizeF(raw.Width, raw.Height);
#elif RPH
            SizeF resolution = Game.Resolution;
#elif SHVDN3
            SizeF resolution = GTA.UI.Screen.Resolution;
#endif

            if (lastKnownResolution != resolution)
            {
                ResolutionChanged?.Invoke(this, new ResolutionChangedEventArgs(lastKnownResolution, resolution));
                RefreshAll();
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
#elif RAGEMP
            float safezone = Invoker.Invoke<float>(Natives.GetSafeZoneSize);
#elif RPH
            float safezone = NativeFunction.CallByHash<float>(0xBAF107B6BB2C97F0);
#elif SHVDN3
            float safezone = Function.Call<float>(Hash.GET_SAFE_ZONE_SIZE);
#endif

            if (lastKnownSafezone != safezone)
            {
                SafezoneChanged?.Invoke(this, new SafeZoneChangedEventArgs(lastKnownSafezone, safezone));
                RefreshAll();
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
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

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
        public void Remove(IProcessable obj) => objects.Remove(obj);
        /// <summary>
        /// Performs the specified action on each element that matches T.
        /// </summary>
        /// <typeparam name="T">The type to match.</typeparam>
        /// <param name="action">The action delegate to perform on each T.</param>
        public void ForEach<T>(Action<T> action)
        {
            foreach (IProcessable obj in new List<IProcessable>(objects))
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
            foreach (IProcessable obj in new List<IProcessable>(objects))
            {
                if (obj is IRecalculable recalculable)
                {
                    recalculable.Recalculate();
                }
            }
        }
        /// <summary>
        /// Hides all of the objects.
        /// </summary>
        public void HideAll()
        {
            foreach (IProcessable obj in new List<IProcessable>(objects))
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
            DetectResolutionChanges();
            DetectSafezoneChanges();

            foreach (IProcessable obj in new List<IProcessable>(objects))
            {
                obj.Process();
            }
        }

        #endregion
    }
}
