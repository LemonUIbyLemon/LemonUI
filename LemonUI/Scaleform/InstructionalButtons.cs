#if FIVEM
using CitizenFX.Core;
using CitizenFX.Core.Native;
#elif RAGEMP
using RAGE.Game;
#elif RPH
using Rage.Native;
using Control = Rage.GameControl;
#elif SHVDN3
using GTA;
using GTA.Native;
#endif
using System;
using System.Collections.Generic;

namespace LemonUI.Scaleform
{

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
