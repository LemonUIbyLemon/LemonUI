#if SHVDN3
using System;

namespace LemonUI.Phone
{
    /// <summary>
    /// Represents the call process when the player initiates a call
    /// </summary>
    public class CalledEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// The contact that was called by the player.
        /// </summary>
        public PhoneContact Contact { get; }
        /// <summary>
        /// The behavior of the call after the wait.
        /// </summary>
        public CallBehavior Behavior { get; set; } = CallBehavior.Busy;
        /// <summary>
        /// The time to wait before the <see cref="Behavior"/> is applied.
        /// </summary>
        public int Wait { get; set; } = 3000;

        #endregion

        #region Constructors

        internal CalledEventArgs(PhoneContact contact)
        {
            Contact = contact;
        }

        #endregion
    }
}
#endif
