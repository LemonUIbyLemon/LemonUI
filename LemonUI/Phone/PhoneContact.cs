#if SHVDN3
using System;

namespace LemonUI.Phone
{
    /// <summary>
    /// A phone contact.
    /// </summary>
    public class PhoneContact
    {
        #region Events

        /// <summary>
        /// Event triggered when the contact is called.
        /// </summary>
        public event EventHandler Called;
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
    }
}
#endif
