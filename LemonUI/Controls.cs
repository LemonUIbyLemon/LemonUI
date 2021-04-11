#if FIVEM
using CitizenFX.Core;
using CitizenFX.Core.Native;
#elif RPH
using Rage.Native;
using Control = Rage.GameControl;
#elif (SHVDN2 || SHVDN3)
using GTA;
using GTA.Native;
#endif
using System.Collections.Generic;

namespace LemonUI
{
    /// <summary>
    /// Tools for dealing with controls.
    /// </summary>
    internal static class Controls
    {
        /// <summary>
        /// Gets if the player used a controller for the last input.
        /// </summary>
        public static bool IsUsingController
        {
            get
            {
#if FIVEM
                return !API.IsInputDisabled(2);
#elif RPH
                return NativeFunction.CallByHash<bool>(0xA571D46727E2B718, 2);
#elif SHVDN2
                return !Function.Call<bool>(Hash._GET_LAST_INPUT_METHOD, 2);
#elif SHVDN3
                return !Function.Call<bool>(Hash._IS_INPUT_DISABLED, 2);
#endif
            }
        }

        /// <summary>
        /// Checks if a control was pressed during the last frame.
        /// </summary>
        /// <param name="control">The control to check.</param>
        /// <returns>true if the control was pressed, false otherwise.</returns>
        public static bool IsJustPressed(Control control)
        {
#if FIVEM
            return API.IsDisabledControlJustPressed(0, (int)control);
#elif RPH
            return NativeFunction.CallByName<bool>("IS_DISABLED_CONTROL_JUST_PRESSED", 0, (int)control);
#elif (SHVDN2 || SHVDN3)
            return Function.Call<bool>(Hash.IS_DISABLED_CONTROL_JUST_PRESSED, 0, (int)control);
#endif
        }

        /// <summary>
        /// Disables all of the controls during the next frame.
        /// </summary>
        public static void DisableAll(int inputGroup = 0)
        {
#if FIVEM
            API.DisableAllControlActions(inputGroup);
#elif RPH
            NativeFunction.CallByName<int>("DISABLE_ALL_CONTROL_ACTIONS", inputGroup);
#elif (SHVDN2 || SHVDN3)
            Function.Call(Hash.DISABLE_ALL_CONTROL_ACTIONS, inputGroup);
#endif
        }

        /// <summary>
        /// Enables a control during the next frame.
        /// </summary>
        /// <param name="control">The control to enable.</param>
        public static void EnableThisFrame(Control control)
        {
#if FIVEM
            API.EnableControlAction(0, (int)control, true);
#elif RPH
            NativeFunction.CallByName<int>("ENABLE_CONTROL_ACTION", 0, (int)control);
#elif (SHVDN2 || SHVDN3)
            Function.Call(Hash.ENABLE_CONTROL_ACTION, 0, (int)control);
#endif
        }

        /// <summary>
        /// Enables a specific set of controls during the next frame.
        /// </summary>
        /// <param name="controls">The controls to enable.</param>
        public static void EnableThisFrame(IEnumerable<Control> controls)
        {
            foreach (Control control in controls)
            {
                EnableThisFrame(control);
            }
        }

        /// <summary>
        /// Disables a control during the next frame.
        /// </summary>
        /// <param name="control">The control to disable.</param>
        public static void DisableThisFrame(Control control)
        {
#if FIVEM
            API.DisableControlAction(0, (int)control, true);
#elif RPH
            NativeFunction.CallByName<int>("DISABLE_CONTROL_ACTION", 0, (int)control, true);
#elif (SHVDN2 || SHVDN3)
            Function.Call(Hash.DISABLE_CONTROL_ACTION, 0, (int)control, true);
#endif
        }
    }
}
