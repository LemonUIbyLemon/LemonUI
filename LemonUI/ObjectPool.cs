#if FIVEMV2
using CitizenFX.FiveM.Native;
#elif FIVEM
using CitizenFX.Core.Native;
#elif RAGEMP
using RAGE.Game;
using RAGE.NUI;
#elif RPH
using Rage;
using Rage.Native;
#elif SHVDN3 || SHVDNC
using GTA.Native;
#elif ALTV
using AltV.Net.Client;
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using LemonUI.Tools;

namespace LemonUI
{
    /// <summary>
    /// Manager for Menus and Items.
    /// </summary>
    public class ObjectPool : IEnumerable<IProcessable>
    {
        #region Fields

        /// <summary>
        /// The last known resolution by the object pool.
        /// </summary>
        private SizeF lastKnownResolution = GameScreen.AbsoluteResolution;
        /// <summary>
        /// The last know Safezone size.
        /// </summary>
        private float lastKnownSafezone = SafeZone.Size;
        /// <summary>
        /// The list of processable objects.
        /// </summary>
        private readonly List<IProcessable> objects = new List<IProcessable>();

        #endregion

        #region Properties

        /// <summary>
        /// Checks if there are objects visible on the screen.
        /// </summary>
        public bool AreAnyVisible
        {
            get
            {
                for (int i = 0; i < objects.Count; i++)
                {
                    if (objects[i].Visible)
                    {
                        return true;
                    }
                }
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
            SizeF resolution = GameScreen.AbsoluteResolution;
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
            float safezone = SafeZone.Size;
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

        #region Functions

        /// <inheritdoc/>
        public IEnumerator<IProcessable> GetEnumerator() => objects.GetEnumerator();
        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
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
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] is T conv)
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
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] is IRecalculable recal)
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
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Visible = false;
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

            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Process();
            }
        }

        #endregion
    }
}
