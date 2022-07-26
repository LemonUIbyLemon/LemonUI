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
