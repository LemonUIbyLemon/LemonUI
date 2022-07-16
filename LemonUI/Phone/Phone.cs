#if SHVDN3
using GTA;
using System.Collections.Generic;

namespace LemonUI.Phone
{
    /// <summary>
    /// Class used to manage the phone.
    /// </summary>
    public class Phone : Script
    {
        #region Fields

        private static readonly List<PhoneContact> contacts = new List<PhoneContact>();

        #endregion

        #region Functions

        /// <summary>
        /// Adds a new contact to the phone.
        /// </summary>
        /// <param name="contact">The contact to add.</param>
        public static void Add(PhoneContact contact)
        {
            if (contacts.Contains(contact))
            {
                return;
            }

            contacts.Add(contact);
        }
        /// <summary>
        /// Removes a contact from the phone.
        /// </summary>
        /// <param name="contact">The contact to remove.</param>
        /// <returns><see langword="true"/> if the contact was removed, <see langword="false"/> otherwise.</returns>
        public static bool Remove(PhoneContact contact) => contacts.Remove(contact);

        #endregion
    }
}
#endif
