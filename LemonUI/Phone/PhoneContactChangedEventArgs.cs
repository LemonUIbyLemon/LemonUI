#if SHVDN3
using System;

namespace LemonUI.Phone
{
    /// <summary>
    /// Represents the event data when one of the contact properties are changed.
    /// </summary>
    public class PhoneContactChangedEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// The <see cref="PhoneContact"/> that was changed.
        /// </summary>
        public PhoneContact Contact { get; }
        /// <summary>
        /// The property that was changed in the contact.
        /// </summary>
        public PhoneInfoChanged Changed { get; }

        #endregion

        #region Constructors

        internal PhoneContactChangedEventArgs(PhoneContact contact, PhoneInfoChanged changed)
        {
            Contact = contact;
            Changed = changed;
        }

        #endregion
    }
}
#endif
