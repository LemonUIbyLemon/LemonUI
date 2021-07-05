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
                return !NativeFunction.CallByHash<bool>(0xA571D46727E2B718, 2);
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
            return NativeFunction.CallByHash<bool>(0x91AEF906BCA88877, 0, (int)control);
#elif (SHVDN2 || SHVDN3)
            return Function.Call<bool>(Hash.IS_DISABLED_CONTROL_JUST_PRESSED, 0, (int)control);
#endif
        }

        /// <summary>
        /// Checks if a control is currently pressed.
        /// </summary>
        /// <param name="control">The control to check.</param>
        /// <returns>true if the control is pressed, false otherwise.</returns>
        public static bool IsPressed(Control control)
        {
#if FIVEM
            return API.IsDisabledControlPressed(0, (int)control);
#elif RPH
            return NativeFunction.CallByHash<bool>(0xE2587F8CBBD87B1D, 0, (int)control);
#elif (SHVDN2 || SHVDN3)
            return Function.Call<bool>(Hash.IS_DISABLED_CONTROL_PRESSED, 0, (int)control);
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
            NativeFunction.CallByHash<int>(0x5F4B6931816E599B, inputGroup);
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
            NativeFunction.CallByHash<int>(0x351220255D64C155, 0, (int)control);
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
            NativeFunction.CallByHash<int>(0xFE99B66D079CF6BC, 0, (int)control, true);
#elif (SHVDN2 || SHVDN3)
            Function.Call(Hash.DISABLE_CONTROL_ACTION, 0, (int)control, true);
#endif
        }
    }
}
