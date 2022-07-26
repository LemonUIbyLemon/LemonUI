#if SHVDN3
using GTA;

namespace LemonUI.Scaleform
{
    /// <summary>
    /// Class used to manage the phone scaleform.
    /// </summary>
    public sealed class Phone : BaseScaleform
    {
        #region Properties

        /// <summary>
        /// The type of phone.
        /// </summary>
        public PhoneType Type { get; }
        /// <summary>
        /// The currently selected index.
        /// </summary>
        public int Index
        {
            get
            {
                int value = CallFunctionReturn("GET_CURRENT_SELECTION");

                while (!IsValueReady(value))
                {
                    Script.Yield();
                }

                return GetValue<int>(value);
            }
        }
        /// <summary>
        /// The total amount of contacts.
        /// </summary>
        /// <remarks>
        /// This will set the contact position to the first element.
        /// </remarks>
        public int Total
        {
            get
            {
                while (Index != 0)
                {
                    Controls.DisableThisFrame(Control.PhoneUp);
                    Controls.DisableThisFrame(Control.PhoneDown);
                    Controls.DisableThisFrame(Control.PhoneLeft);
                    Controls.DisableThisFrame(Control.PhoneRight);
                    Controls.DisableThisFrame(Control.PhoneSelect);
                    Controls.DisableThisFrame(Control.PhoneCancel);
                    Controls.DisableThisFrame(Control.PhoneOption);
                    CallFunction("SET_INPUT_EVENT", 3);
                }

                CallFunction("SET_INPUT_EVENT", 1);
                int last = Index;
                CallFunction("SET_INPUT_EVENT", 3);
                return last + 1;
            }
        }

        #endregion
        
        #region Constructors

        /// <inheritdoc/>
        public Phone(PhoneType type) : base(GetName(type))
        {
            Type = type;
        }

        #endregion

        #region Functions

        private static string GetName(PhoneType type)
        {
            switch (type)
            {
                case PhoneType.Badger:
                    return "cellphone_badger";
                case PhoneType.Facade:
                    return "cellphone_facade";
                case PhoneType.IFruit:
                    return "cellphone_ifruit";
                default:
                    return "cellphone_ifruit";
            }
        }
        /// <inheritdoc/>
        public override void Update()
        {
        }
        /// <summary>
        /// Shows a specific page on the phone.
        /// </summary>
        /// <param name="page">The page ID to show.</param>
        public void ShowPage(int page) => CallFunction("DISPLAY_VIEW", page, 0);
        /// <summary>
        /// Adds a contact at the specified location.
        /// </summary>
        /// <param name="position">The position to add the contact at.</param>
        /// <param name="name">The name of the contact.</param>
        /// <param name="icon">The icon to use.</param>
        public void AddContactAt(int position, string name, string icon)
        {
            CallFunction("SET_DATA_SLOT", 2, position, 0, name, string.Empty, icon);
        }
        /// <summary>
        /// Shows the calling screen.
        /// </summary>
        /// <param name="name">The name of the contact.</param>
        /// <param name="dialingLabel">The label used to dial up.</param>
        /// <param name="icon">The icon to use.</param>
        public void ShowCalling(string name, string dialingLabel, string icon)
        {
            CallFunction("SET_DATA_SLOT", 4, 0, 3, name, icon, Game.GetLocalizedString(dialingLabel));
            CallFunction("DISPLAY_VIEW", 4);
        }

        #endregion
    }
}
#endif
