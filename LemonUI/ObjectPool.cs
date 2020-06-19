#if FIVEM
using CitizenFX.Core;
using CitizenFX.Core.UI;
using CitizenFX.Core.Native;
#elif SHVDN2
using GTA;
using GTA.Native;
#elif SHVDN3
using GTA;
using GTA.Native;
using GTA.UI;
#endif
using LemonUI.Menus;
using System;
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
        /// The last know Safezone size.
        /// </summary>
#if FIVEM
        private float lastKnownSafezone = API.GetSafeZoneSize();
#else
        private float lastKnownSafezone = Function.Call<float>(Hash.GET_SAFE_ZONE_SIZE);
#endif
        /// <summary>
        /// The list of processable objects.
        /// </summary>
        private readonly List<IProcessable> objects = new List<IProcessable>();
        /// <summary>
        /// The menus that are part of this pool.
        /// </summary>
        private readonly List<IMenu> menus = new List<IMenu>();
        /// <summary>
        /// The controls required by the menu with both a gamepad and mouse + keyboard.
        /// </summary>
        private readonly List<Control> controlsRequired = new List<Control>
        {
            Control.FrontendAccept,
            Control.FrontendAxisX,
            Control.FrontendAxisY,
            Control.FrontendDown,
            Control.FrontendUp,
            Control.FrontendLeft,
            Control.FrontendRight,
            Control.FrontendCancel,
            Control.FrontendSelect,
            Control.CursorScrollDown,
            Control.CursorScrollUp,
            Control.CursorX,
            Control.CursorY,
            Control.MoveUpDown,
            Control.MoveLeftRight,
            Control.Sprint,
            Control.Jump,
            Control.Enter,
            Control.VehicleExit,
            Control.VehicleAccelerate,
            Control.VehicleBrake,
            Control.VehicleMoveLeftRight,
            Control.VehicleFlyYawLeft,
            Control.FlyLeftRight,
            Control.FlyUpDown,
            Control.VehicleFlyYawRight,
            Control.VehicleHandbrake
        };
        /// <summary>
        /// The controls required in a gamepad.
        /// </summary>
        private readonly List<Control> controlsGamepad = new List<Control>
        {
            Control.LookUpDown,
            Control.LookLeftRight,
            Control.Aim,
            Control.Attack
        };

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
        /// <summary>
        /// Detects Safezone changes by comparing the last known value to the current one.
        /// </summary>
        private void DetectSafezoneChanges()
        {
            // Get the current Safezone size
#if FIVEM
            float safezone = API.GetSafeZoneSize();
#else
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
            // Don't allow null objects
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            // If is a menu, add it to the menus
            if (obj is IMenu menu)
            {
                if (menus.Contains(menu))
                {
                    throw new InvalidOperationException("The menu is already part of this pool.");
                }

                menus.Add(menu);
            }
            // Otherwise, add it to the general pool
            else
            {
                if (objects.Contains(obj))
                {
                    throw new InvalidOperationException("The object is already part of this pool.");
                }

                objects.Add(obj);
            }
        }
        /// <summary>
        /// Removes the object from the pool.
        /// </summary>
        /// <param name="obj">The object to remove.</param>
        public void Remove(IProcessable obj)
        {
            menus.Remove(obj as IMenu);
            objects.Remove(obj);
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
            // And do the same with the menus
            foreach (IMenu menu in objects)
            {
                menu.Recalculate();
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

            // Iterate over the menus
            foreach (IMenu menu in menus)
            {
                // If the menu is visible
                if (menu.Visible)
                {
                    // Disable all of the controls
                    Controls.DisableAll(2);
                    // And enable only those required
                    Controls.EnableThisFrame(controlsRequired);
                    // If we are using a controller, also enable those required by a gamepad
                    if (Controls.IsUsingController)
                    {
                        Controls.EnableThisFrame(controlsGamepad);
                    }
                    // And break the iterator
                    break;
                }
            }

            // Check if the controls necessary were pressed
            bool backPressed = Controls.IsJustPressed(Control.PhoneCancel) || Controls.IsJustPressed(Control.FrontendPause);

            // Then go ahead and process the menus
            foreach (IMenu menu in menus)
            {
                // If the menu should be visible on the screen
                if (menu.Visible)
                {
                    // Process it
                    menu.Process();

                    // If the player pressed the back button
                    if (backPressed)
                    {
                        menu.Visible = false;
                    }
                }
            }

            // And finally process all other objects
            foreach (IProcessable obj in objects)
            {
                obj.Process();
            }
        }

        #endregion
    }
}
