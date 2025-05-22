#if SHVDN3
using System;

namespace LemonUI.Phone
{
    /// <summary>
    /// Represents the call process when the player answers the phone and the call is connected.
    /// </summary>
    public class ConnectedEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// The contact that was called by the player.
        /// </summary>
        public PhoneContact Contact { get; }
        /// <summary>
        /// After how much time should the call be disconnected automatically.
        /// </summary>
        public int DisconnectAfter { get; set; }

        #endregion

        #region Constructors

        internal ConnectedEventArgs(PhoneContact contact)
        {
            Contact = contact;
        }

        #endregion
    }
}
#endif
