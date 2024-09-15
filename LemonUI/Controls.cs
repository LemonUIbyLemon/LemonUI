#if FIVEMV2
using CitizenFX.FiveM;
using CitizenFX.FiveM.Native;
#elif FIVEM
using CitizenFX.Core;
using CitizenFX.Core.Native;
#elif ALTV
using AltV.Net.Client;
#elif RAGEMP
using RAGE.Game;
#elif RPH
using Rage.Native;
using Control = Rage.GameControl;
#elif SHVDN3 || SHVDNC
using GTA;
using GTA.Native;
#endif
using System.Collections.Generic;
using LemonUI.Elements;

namespace LemonUI
{
    /// <summary>
    /// Tools for dealing with controls.
    /// </summary>
    internal static class Controls
    {
        #region Properties

        /// <summary>
        /// Gets if the player used a controller for the last input.
        /// </summary>
        public static bool IsUsingController
        {
            get
            {
#if FIVEMV2
                return !Natives.IsInputDisabled(2);
#elif FIVEM
                 return !API.IsInputDisabled(2);
#elif ALTV
                return !Alt.Natives.IsUsingKeyboardAndMouse(2);
#elif RAGEMP
                return !Invoker.Invoke<bool>(Natives.IsInputDisabled, 2);
#elif RPH
                return !NativeFunction.CallByHash<bool>(0xA571D46727E2B718, 2);
#elif SHVDN3 || SHVDNC
                return !Function.Call<bool>(Hash.IS_USING_KEYBOARD_AND_MOUSE, 2);
#endif
            }
        }

        #endregion

        #region Functions

        /// <summary>
        /// Checks if a control was pressed during the last frame.
        /// </summary>
        /// <param name="control">The control to check.</param>
        /// <returns>true if the control was pressed, false otherwise.</returns>
        public static bool IsJustPressed(Control control)
        {
#if FIVEMV2
            return Natives.IsDisabledControlJustPressed(0, (int)control);
#elif FIVEM
            return API.IsDisabledControlJustPressed(0, (int)control);
#elif ALTV
            return Alt.Natives.IsDisabledControlJustPressed(0, (int)control);
#elif RAGEMP
            return Invoker.Invoke<bool>(Natives.IsDisabledControlJustPressed, 0, (int)control);
#elif RPH
            return NativeFunction.CallByHash<bool>(0x91AEF906BCA88877, 0, (int)control);
#elif SHVDN3 || SHVDNC
            return Function.Call<bool>(Hash.IS_DISABLED_CONTROL_JUST_PRESSED, 0, (int)control);
#elif ALTV
            return Alt.Natives.IsControlJustPressed((uint)control);
#endif
        }
        /// <summary>
        /// Checks if a control is currently pressed.
        /// </summary>
        /// <param name="control">The control to check.</param>
        /// <returns>true if the control is pressed, false otherwise.</returns>
        public static bool IsPressed(Control control)
        {
#if FIVEMV2
            return Natives.IsDisabledControlPressed(0, (int)control);
#elif FIVEM
            return API.IsDisabledControlPressed(0, (int)control);
#elif ALTV
            return Alt.Natives.IsDisabledControlPressed(0, (int)control);
#elif RAGEMP
            return Invoker.Invoke<bool>(Natives.IsDisabledControlJustPressed, 0, (int)control);
#elif RPH
            return NativeFunction.CallByHash<bool>(0xE2587F8CBBD87B1D, 0, (int)control);
#elif SHVDN3 || SHVDNC
            return Function.Call<bool>(Hash.IS_DISABLED_CONTROL_PRESSED, 0, (int)control);
#endif
        }
        /// <summary>
        /// Disables all of the controls during the next frame.
        /// </summary>
        public static void DisableAll(int inputGroup = 0)
        {
#if FIVEMV2
            Natives.DisableAllControlActions(inputGroup);
#elif FIVEM
            API.DisableAllControlActions(inputGroup);
#elif ALTV
            Alt.Natives.DisableAllControlActions(inputGroup);
#elif RAGEMP
            Invoker.Invoke(Natives.DisableAllControlActions, inputGroup);
#elif RPH
            NativeFunction.CallByHash<int>(0x5F4B6931816E599B, inputGroup);
#elif SHVDN3 || SHVDNC
            Function.Call(Hash.DISABLE_ALL_CONTROL_ACTIONS, inputGroup);
#endif
        }
        /// <summary>
        /// Enables a control during the next frame.
        /// </summary>
        /// <param name="control">The control to enable.</param>
        public static void EnableThisFrame(Control control)
        {
#if FIVEMV2
            Natives.EnableControlAction(0, (int)control, true);
#elif FIVEM
            API.EnableControlAction(0, (int)control, true);
#elif ALTV
            Alt.Natives.EnableControlAction(0, (int)control, true);
#elif RAGEMP
            Invoker.Invoke(Natives.EnableControlAction, 0, (int)control, true);
#elif RPH
            NativeFunction.CallByHash<int>(0x351220255D64C155, 0, (int)control);
#elif SHVDN3 || SHVDNC
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
#if FIVEMV2
            Natives.DisableControlAction(0, (int)control, true);
#elif FIVEM
            API.DisableControlAction(0, (int)control, true);
#elif ALTV
            Alt.Natives.DisableControlAction(0, (int)control, true);
#elif RAGEMP
            Invoker.Invoke(Natives.DisableControlAction, 0, (int)control, true);
#elif RPH
            NativeFunction.CallByHash<int>(0xFE99B66D079CF6BC, 0, (int)control, true);
#elif SHVDN3 || SHVDNC
            Function.Call(Hash.DISABLE_CONTROL_ACTION, 0, (int)control, true);
#endif
        }

        #endregion
    }
}
