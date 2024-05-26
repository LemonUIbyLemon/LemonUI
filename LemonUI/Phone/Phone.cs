#if SHVDN3
using GTA;
using GTA.Native;
using System;
using System.Collections.Generic;
using LemonUI.Scaleform;

namespace LemonUI.Phone
{
    /// <summary>
    /// Class used to manage the phone.
    /// </summary>
    public class PhoneManager : Script
    {
        #region Fields

        private static readonly List<PhoneContact> contacts = new List<PhoneContact>();
        private static int total = -1;
        private static Scaleform.Phone badger = new Scaleform.Phone(PhoneType.Badger);
        private static Scaleform.Phone facade = new Scaleform.Phone(PhoneType.Facade);
        private static Scaleform.Phone ifruit = new Scaleform.Phone(PhoneType.IFruit);

        #endregion

        #region Properties

        private static Scaleform.Phone CurrentPhone
        {
            get
            {
                switch ((uint)Game.Player.Character.Model.Hash)
                {
                    case 0x9B22DBAF: // Franklin
                        return badger;
                    case 0x0D7114C9: // Michael
                        return ifruit;
                    case 0x9B810FA2: // Trevor
                        return facade;
                    default:
                        return ifruit;
                }
            }
        }
        /// <summary>
        /// If the phone's contact are open on the screen.
        /// </summary>
        public static bool AreContactsOpen => Function.Call<bool>(Hash._GET_NUMBER_OF_REFERENCES_OF_SCRIPT_WITH_NAME_HASH, Game.GenerateHash("appcontacts"));
        /// <summary>
        /// The contact currently being called.
        /// </summary>
        public static PhoneContact Current { get; private set; }

        #endregion

        #region Constructors

        public PhoneManager()
        {
            Tick += Process;
        }

        #endregion

        #region Functions

        private static void Process(object sender, EventArgs e)
        {
            Scaleform.Phone currentPhone = CurrentPhone;

            if (Current != null)
            {
                Current.Process(currentPhone);

                if (!Current.IsActive)
                {
                    Current = null;
                }
                else
                {
                    return;
                }
            }

            if (AreContactsOpen)
            {
                if (total == -1)
                {
                    total = currentPhone.Total;
                }

                int index = currentPhone.Index;

                if (Game.IsControlPressed(Control.PhoneSelect) && index >= total)
                {
                    Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "appcontacts");
                    Current = contacts[index - total];
                    Current.Call(currentPhone);
                    return;
                }

                for (int i = 0; i < contacts.Count; i++)
                {
                    PhoneContact contact = contacts[i];
                    currentPhone.AddContactAt(total + i, contact.Name, contact.Icon);
                }
            }
            else
            {
                total = -1;
            }
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
