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
using System;
using System.Collections.Generic;

namespace LemonUI.Scaleform
{
    /// <summary>
    /// An individual instructional button.
    /// </summary>
    public struct InstructionalButton
    {
        #region Private Fields

        private Control control;
        private string raw;

        #endregion

        #region Public Properties

        /// <summary>
        /// The description of this button.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The Control used by this button.
        /// </summary>
        public Control Control
        {
            get => control;
            set
            {
                control = value;
#if FIVEM
                raw = API.GetControlInstructionalButton(2, (int)value, 1);
#elif SHVDN2
                raw = Function.Call<string>(Hash._0x0499D7B09FC9B407, 2, (int)value, 1);
#elif SHVDN3
                raw = Function.Call<string>(Hash.GET_CONTROL_INSTRUCTIONAL_BUTTON, 2, (int)value, 1);
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
                raw = value;
                control = (Control)(-1);
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instructional button for a Control.
        /// </summary>
        /// <param name="description">The text for the description.</param>
        /// <param name="control">The control to use.</param>
        public InstructionalButton(string description, Control control)
        {
            Description = description;
            this.control = control;
#if FIVEM
            raw = API.GetControlInstructionalButton(2, (int)control, 1);
#elif RPH
            raw = (string)NativeFunction.CallByHash(0x0499D7B09FC9B407, typeof(string), 2, (int)control, 1);
#elif SHVDN2
            raw = Function.Call<string>(Hash._0x0499D7B09FC9B407, 2, (int)control, 1);
#elif SHVDN3
            raw = Function.Call<string>(Hash.GET_CONTROL_INSTRUCTIONAL_BUTTON, 2, (int)control, 1);
#endif
        }

        /// <summary>
        /// Creates an instructional button for a raw control.
        /// </summary>
        /// <param name="description">The text for the description.</param>
        /// <param name="raw">The raw value of the control.</param>
        public InstructionalButton(string description, string raw)
        {
            Description = description;
            control = (Control)(-1);
            this.raw = raw;
        }

        #endregion
    }

    /// <summary>
    /// Buttons shown on the bottom right of the screen.
    /// </summary>
    public class InstructionalButtons : BaseScaleform
    {
        #region Public Fields

        /// <summary>
        /// The buttons used in this Scaleform.
        /// </summary>
        private readonly List<InstructionalButton> buttons = new List<InstructionalButton>();

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new set of Instructional Buttons.
        /// </summary>
        /// <param name="buttons">The buttons to add into this menu.</param>
        public InstructionalButtons(params InstructionalButton[] buttons) : base("INSTRUCTIONAL_BUTTONS")
        {
            this.buttons.AddRange(buttons);
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Adds an Instructional Button.
        /// </summary>
        /// <param name="button">The button to add.</param>
        public void Add(InstructionalButton button)
        {
            // If the item is already in the list, raise an exception
            if (buttons.Contains(button))
            {
                throw new InvalidOperationException("The button is already in the Scaleform.");
            }

            // Otherwise, add it to the list of items
            buttons.Add(button);
        }
        /// <summary>
        /// Removes an Instructional Button.
        /// </summary>
        /// <param name="button">The button to remove.</param>
        public void Remove(InstructionalButton button)
        {
            // If the button is not in the list, return
            if (!buttons.Contains(button))
            {
                return;
            }

            // Otherwise, remove it
            buttons.Remove(button);
        }
        /// <summary>
        /// Removes all of the instructional buttons.
        /// </summary>
        public void Clear()
        {
            buttons.Clear();
        }
        /// <summary>
        /// Refreshes the items shown in the Instructional buttons.
        /// </summary>
        public override void Update()
        {
            // Clear all of the existing items
            CallFunction("CLEAR_ALL");
            CallFunction("TOGGLE_MOUSE_BUTTONS", 0);
            CallFunction("CREATE_CONTAINER");

            // And add them again
            for (int i = 0; i < buttons.Count; i++)
            {
                InstructionalButton button = buttons[i];
                CallFunction("SET_DATA_SLOT", i, button.Raw, button.Description);
            }

            CallFunction("DRAW_INSTRUCTIONAL_BUTTONS", -1);
        }

        #endregion
    }
}
