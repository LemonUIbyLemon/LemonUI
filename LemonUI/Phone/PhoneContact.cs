#if SHVDN3
using GTA;
using System;
using GTA.Native;

namespace LemonUI.Phone
{
    /// <summary>
    /// A phone contact.
    /// </summary>
    public class PhoneContact
    {
        #region Fields

        private const string dialingLabel = "CELL_211";
        private const string busyLabel = "CELL_220";
        private const string connectedLabel = "CELL_219";
        private static readonly Sound busySound = new Sound("Phone_SoundSet_Default", "Remote_Engaged");
        private static readonly Sound callingSound = new Sound("Phone_SoundSet_Default", "Dial_and_Remote_Ring");
        private CalledEventArgs called;
        private string label = dialingLabel;
        private CallStatus status = CallStatus.Inactive;
        private int waitUntil = -1;

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when the contact is called by the player.
        /// </summary>
        public event CalledEventHandler Called;
        /// <summary>
        /// Event triggered when the call is answered by the player.
        /// </summary>
        /// <remarks>
        /// This event will trigger both, when the player calls the contact and when the contact calls the player.
        /// </remarks>
        public event ConnectedEventHandler Connected;
        /// <summary>
        /// Event triggered when the call finishes naturally, without player input.
        /// </summary>
        public event EventHandler Finished;

        #endregion

        #region Properties

        /// <summary>
        /// The name of the contact.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The icon of the contact.
        /// </summary>
        public string Icon { get; set; } = "CHAR_DEFAULT";
        /// <summary>
        /// If this phone contact is currently active.
        /// </summary>
        public bool IsActive => status != CallStatus.Inactive;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new contact.
        /// </summary>
        /// <param name="name">The name of the contact.</param>
        public PhoneContact(string name)
        {
            Name = name;
        }

        #endregion

        #region Tools

        private static void RestoreScript(string script, int stack)
        {
            if (Function.Call<bool>(Hash._GET_NUMBER_OF_REFERENCES_OF_SCRIPT_WITH_NAME_HASH, Game.GenerateHash("appcontacts") > 0))
            {
                return;
            }

            while (!Function.Call<bool>(Hash.HAS_SCRIPT_LOADED, script))
            {
                Function.Call(Hash.REQUEST_SCRIPT, script);
            }

            Function.Call(Hash.START_NEW_SCRIPT, script, stack);
        }

        #endregion

        #region Functions

        private static void Restart()
        {
            Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "cellphone_flashhand");
            Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "cellphone_controller");
            RestoreScript("cellphone_flashhand", 1424);
            RestoreScript("cellphone_controller", 1424);
        }
        internal void Call(Scaleform.Phone phone)
        {
            status = CallStatus.Idle;
            Process(phone);
        }
        internal void Process(Scaleform.Phone phone)
        {
            phone.SetButtonIcon(1, 1);
            phone.SetButtonIcon(2, 1);
            phone.SetButtonIcon(3, 6);

            phone.ShowCalling(Name, label, Icon);

            if (status == CallStatus.Idle)
            {
                callingSound.PlayFrontend(false);

                Game.Player.Character.Task.UseMobilePhone();
                called = new CalledEventArgs(this);
                Called?.Invoke(phone, called);
                waitUntil = Game.GameTime + called.Wait;
                status = CallStatus.Calling;
            }

            if (status == CallStatus.Calling)
            {
                if (waitUntil < Game.GameTime)
                {
                    callingSound.Stop();
                    status = CallStatus.Called;
                    waitUntil = -1;
                }

                if (Game.IsControlJustPressed(Control.PhoneCancel))
                {
                    callingSound.Stop();
                    status = CallStatus.Inactive;
                    waitUntil = -1;
                    return;
                }
            }

            if (status == CallStatus.Called)
            {
                switch (called.Behavior)
                {
                    case CallBehavior.Busy:
                        busySound.PlayFrontend(false);
                        status = CallStatus.Busy;
                        label = busyLabel;
                        break;
                    case CallBehavior.Available:
                        ConnectedEventArgs connected = new ConnectedEventArgs(this);
                        Connected?.Invoke(phone, connected);
                        waitUntil = Game.GameTime + connected.DisconnectAfter;
                        status = CallStatus.Connected;
                        label = connectedLabel;
                        break;
                }

                Game.Player.Character.Task.PutAwayMobilePhone();
            }

            if (status == CallStatus.Busy)
            {
                if (Game.IsControlPressed(Control.PhoneCancel))
                {
                    busySound.Stop();
                    phone.ShowPage(2);
                    RestoreScript("appContacts", 4000);
                    status = CallStatus.Inactive;
                    return;
                }
            }

            if (status == CallStatus.Connected &&
                (waitUntil < Game.GameTime || Game.IsControlPressed(Control.PhoneCancel)))
            {
                status = CallStatus.Inactive;
                waitUntil = -1;
                Restart();
            }
        }

        #endregion
    }
}
#endif
