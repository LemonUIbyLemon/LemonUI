#if FIVEM
using CitizenFX.Core.UI;
#elif SHVDN2
using GTA;
using System;
#elif SHVDN3
using GTA.UI;
#endif
using System.Collections.Generic;
using System.Drawing;

namespace LemonUI
{
    /// <summary>
    /// Manager for Menus and Items.
    /// </summary>
    public class ObjectPool : IProcessable
    {
        #region Private Fields

        /// <summary>
        /// The last known resolution by the object pool.
        /// </summary>
#if SHVDN2
        private SizeF lastKnownResolution = Game.ScreenResolution;
#else
        private SizeF lastKnownResolution = Screen.Resolution;
#endif
        /// <summary>
        /// The list of processable objects.
        /// </summary>
        private readonly List<IProcessable> objects = new List<IProcessable>();

        #endregion

        #region Public Properties

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when the game resolution is changed.
        /// </summary>
        public event ResolutionChangedEventHandler ResolutionChanged;

        #endregion

        #region Constructor

        #endregion

        #region Tools

        /// <summary>
        /// Detects resolution changes by comparing the last known resolution and the current one.
        /// </summary>
        private void DetectResolutionChanges()
        {
            // Get the current resolution
#if SHVDN2
            SizeF resolution = Game.ScreenResolution;
#else
            SizeF resolution = Screen.Resolution;
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

        #endregion

        #region Public Function

        /// <summary>
        /// Adds the object into the pool.
        /// </summary>
        /// <param name="obj">The object to add.</param>
        public void Add(IProcessable obj)
        {
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
        /// Refreshes all of the items.
        /// </summary>
        public void RefreshAll()
        {
            // Iterate over the objects
            foreach (IProcessable obj in objects)
            {
                // If it can be recalculated, do it
                if (obj is IRecalculable recal)
                {
                    recal.Recalculate();
                }
            }
        }
        /// <summary>
        /// Processes the objects in the pool.
        /// </summary>
        public void Process()
        {
            // See if there is a resolution change
            DetectResolutionChanges();

            // Then go ahead and process all of the objects
            foreach (IProcessable obj in objects)
            {
                obj.Process();
            }
        }

        #endregion
    }
}
