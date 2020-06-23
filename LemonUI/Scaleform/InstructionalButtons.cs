#if FIVEM
using CitizenFX.Core;
using CitizenFX.Core.Native;
#else
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
        public InstructionalButtons(params InstructionalButton[] buttons)
        {
            // Request the scaleform
#if FIVEM
            scaleform = API.RequestScaleformMovie("INSTRUCTIONAL_BUTTONS");
#else
            scaleform = Function.Call<int>(Hash.REQUEST_SCALEFORM_MOVIE, "INSTRUCTIONAL_BUTTONS");
#endif
            // Add the buttons to the list
            this.buttons.AddRange(buttons);
            // And refresh the items in the scaleform itself
            Refresh();
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

            // Otherwise, add it and refresh the items
            buttons.Add(button);
            Refresh();
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

            // Otherwise, remove it and refresh the items
            buttons.Remove(button);
            Refresh();
        }
        /// <summary>
        /// Removes all of the instructional buttons.
        /// </summary>
        public void Clear()
        {
            buttons.Clear();
            Refresh();
        }
        /// <summary>
        /// Refreshes the items shown in the Instructional buttons.
        /// </summary>
        public void Refresh()
        {
            // Clear all of the existing items
#if FIVEM
            API.CallScaleformMovieMethod(scaleform, "CLEAR_ALL");
            API.CallScaleformMovieMethod(scaleform, "CREATE_CONTAINER");
#elif SHVDN2
            Function.Call(Hash._CALL_SCALEFORM_MOVIE_FUNCTION_VOID, scaleform, "CLEAR_ALL");
            Function.Call(Hash._CALL_SCALEFORM_MOVIE_FUNCTION_VOID, scaleform, "CREATE_CONTAINER");
#elif SHVDN3
            Function.Call(Hash.CALL_SCALEFORM_MOVIE_METHOD, scaleform, "CLEAR_ALL");
            Function.Call(Hash.CALL_SCALEFORM_MOVIE_METHOD, scaleform, "CREATE_CONTAINER");
#endif

            // And add them again
            for (int i = 0; i < buttons.Count; i++)
            {
                InstructionalButton button = buttons[i];
#if FIVEM
                API.BeginScaleformMovieMethod(scaleform, "SET_DATA_SLOT");
                API.ScaleformMovieMethodAddParamInt(i);
                API.ScaleformMovieMethodAddParamPlayerNameString(button.Raw);
                API.ScaleformMovieMethodAddParamPlayerNameString(button.Description);
                API.EndScaleformMovieMethod();
#elif SHVDN2
                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, scaleform, "SET_DATA_SLOT");
                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, i);
                Function.Call(Hash._0xE83A3E3557A56640, button.Raw);
                Function.Call(Hash._0xE83A3E3557A56640, button.Description);
                Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);
#elif SHVDN3
                Function.Call(Hash.BEGIN_SCALEFORM_MOVIE_METHOD, scaleform, "SET_DATA_SLOT");
                Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, i);
                Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_PLAYER_NAME_STRING, button.Raw);
                Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_PLAYER_NAME_STRING, button.Description);
                Function.Call(Hash.END_SCALEFORM_MOVIE_METHOD);
#endif
            }

#if FIVEM
            API.BeginScaleformMovieMethod(scaleform, "DRAW_INSTRUCTIONAL_BUTTONS");
            API.ScaleformMovieMethodAddParamInt(-1);
            API.EndScaleformMovieMethod();
#elif SHVDN2
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, scaleform, "DRAW_INSTRUCTIONAL_BUTTONS");
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, -1);
            Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);
#elif SHVDN3
            Function.Call(Hash.BEGIN_SCALEFORM_MOVIE_METHOD, scaleform, "DRAW_INSTRUCTIONAL_BUTTONS");
            Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, -1);
            Function.Call(Hash.END_SCALEFORM_MOVIE_METHOD);
#endif
        }

        #endregion
    }
}
