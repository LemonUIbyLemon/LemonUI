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

        #region Constructors

        public Phone()
        {
            Tick += (sender, e) => Process();
        }

        #endregion

        #region Functions

        private static void Process()
        {
        }
        private static void HandleChanges(object sender, PhoneContactChangedEventArgs e)
        {
        }
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

            contact.Changed += HandleChanges;
            contacts.Add(contact);
        }
        /// <summary>
        /// Removes a contact from the phone.
        /// </summary>
        /// <param name="contact">The contact to remove.</param>
        /// <returns><see langword="true"/> if the contact was removed, <see langword="false"/> otherwise.</returns>
        public static bool Remove(PhoneContact contact)
        {
            if (!contacts.Contains(contact))
            {
                return false;
            }

            contact.Changed -= HandleChanges;
            return contacts.Remove(contact);
        }

        #endregion
    }
}
#endif
