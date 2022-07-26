#if SHVDN3
using GTA;
using System;

namespace LemonUI.Phone
{
    /// <summary>
    /// A phone contact.
    /// </summary>
    public class PhoneContact
    {
        #region Fields

        private static readonly Sound busySound = new Sound("Phone_SoundSet_Default", "Remote_Engaged");
        private static readonly Sound callingSound = new Sound("Phone_SoundSet_Default", "Dial_and_Remote_Ring");

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
        public event EventHandler Answered;
        /// <summary>
        /// Event triggered when the call is hung up by the player.
        /// </summary>
        public event EventHandler Cancelled;
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

        #region Functions

        internal void Call(Scaleform.Phone phone)
        {
            const string dialing = "CELL_211";
            const string busy = "CELL_220";
            const string connected = "CELL_219";

            callingSound.PlayFrontend(false);
            phone.ShowCalling(Name, dialing, Icon);

            CalledEventArgs called = new CalledEventArgs(this);
            Called?.Invoke(phone, called);

            int waitUntil = Game.GameTime + called.Wait;

            while (waitUntil > Game.GameTime)
            {
                phone.ShowCalling(Name, dialing, Icon);

                if (Game.IsControlJustPressed(Control.PhoneCancel))
                {
                    callingSound.Stop();
                    return;
                }

                Script.Yield();
            }

            callingSound.Stop();

            switch (called.Behavior)
            {
                case CallBehavior.Busy:
                    busySound.PlayFrontend(false);
                    while (!Game.IsControlPressed(Control.PhoneCancel))
                    {
                        phone.ShowCalling(Name, busy, Icon);
                        Script.Yield();
                    }
                    busySound.Stop();
                    break;
                case CallBehavior.Available:
                    phone.ShowCalling(Name, connected, Icon);
                    Answered?.Invoke(phone, EventArgs.Empty);
                    break;
            }
        }

        #endregion
    }
}
#endif
