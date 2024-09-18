#if FIVEMV2
using CitizenFX.FiveM;
using CitizenFX.FiveM.Native;
#elif FIVEM
using CitizenFX.Core;
using CitizenFX.Core.Native;
#elif RAGEMP
using RAGE.Game;
#elif ALTV
using AltV.Net.Client;
using LemonUI.Elements;
#elif RPH
using Rage.Native;
using Control = Rage.GameControl;
#elif SHVDN3 || SHVDNC
using GTA;
using GTA.Native;
#endif
using System;

namespace LemonUI.Scaleform
{
    /// <summary>
    /// An individual instructional button.
    /// </summary>
    public struct InstructionalButton
    {
        #region Fields

        private Control control;
        private string raw;
        private string description;

        #endregion

        #region Properties

        /// <summary>
        /// The description of this button.
        /// </summary>
        public string Description
        {
            get => description;
            set => description = value ?? throw new ArgumentNullException(nameof(value));
        }
        /// <summary>
        /// The Control used by this button.
        /// </summary>
        public Control Control
        {
            get => control;
            set
            {
                control = value;
#if FIVEMV2
                raw = Natives.GetControlInstructionalButton(2, (int)value, true);
#elif FIVEM
                raw = API.GetControlInstructionalButton(2, (int)value, 1);
#elif RAGEMP
                raw = Invoker.Invoke<string>(Natives.GetControlInstructionalButton, 2, (int)value, 1);
#elif RPH
                raw = (string)NativeFunction.CallByHash(0x0499D7B09FC9B407, typeof(string), 2, (int)control, 1);
#elif SHVDN3 || SHVDNC
                raw = Function.Call<string>(Hash.GET_CONTROL_INSTRUCTIONAL_BUTTONS_STRING, 2, (int)value, 1);
#elif ALTV
                raw = Alt.Natives.GetControlInstructionalButtonsString(2, (int)value, true);
#endif
            }
        }
        /// <summary>
        /// The Raw Control sent to the Scaleform.
        /// </summary>
        public string Raw
        {
            get => raw;
            set
            {
                raw = value ?? throw new ArgumentNullException(nameof(value));
                control = (Control)(-1);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an instructional button for a Control.
        /// </summary>
        /// <param name="description">The text for the description.</param>
        /// <param name="control">The control to use.</param>
        public InstructionalButton(string description, Control control)
        {
            this.description = description ?? throw new ArgumentNullException(nameof(description));
            this.control = control;
#if FIVEMV2
            raw = Natives.GetControlInstructionalButton(2, (int)control, true);
#elif FIVEM
            raw = API.GetControlInstructionalButton(2, (int)control, 1);
#elif ALTV
            raw = Alt.Natives.GetControlInstructionalButtonsString(2, (int)control, true);
#elif RAGEMP
            raw = Invoker.Invoke<string>(Natives.GetControlInstructionalButton, 2, (int)control, 1);
#elif RPH
            raw = (string)NativeFunction.CallByHash(0x0499D7B09FC9B407, typeof(string), 2, (int)control, 1);
#elif SHVDN3 || SHVDNC
            raw = Function.Call<string>(Hash.GET_CONTROL_INSTRUCTIONAL_BUTTONS_STRING, 2, (int)control, 1);
#endif
        }
        /// <summary>
        /// Creates an instructional button for a raw control.
        /// </summary>
        /// <param name="description">The text for the description.</param>
        /// <param name="raw">The raw value of the control.</param>
        public InstructionalButton(string description, string raw)
        {
            this.description = description ?? throw new ArgumentNullException(nameof(description));
            control = (Control)(-1);
            this.raw = raw ?? throw new ArgumentNullException(nameof(raw));
        }

        #endregion
    }
}
