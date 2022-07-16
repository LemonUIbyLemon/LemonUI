#if SHVDN3
using System;

namespace LemonUI.Phone
{
    /// <summary>
    /// A phone contact.
    /// </summary>
    public class PhoneContact
    {
        #region Fields

        private string name = string.Empty;
        private string pictureDictionary = string.Empty;
        private string pictureTexture = string.Empty;

        #endregion

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
        /// <summary>
        /// Event triggered when the picture or name of the contact changes.
        /// </summary>
        public event PhoneContactChangedEventHandler Changed;

        #endregion

        #region Properties

        /// <summary>
        /// The name of the contact.
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                if (name == value)
                {
                    return;
                }

                name = value;
                OnChanged(PhoneInfoChanged.Name);
            }
        }
        /// <summary>
        /// The dictionary where the contact picture is located.
        /// </summary>
        public string PictureDictionary
        {
            get => pictureDictionary;
            set
            {
                if (pictureDictionary == value)
                {
                    return;
                }

                pictureDictionary = value;
                OnChanged(PhoneInfoChanged.Picture);
            }
        }
        /// <summary>
        /// The texture inside of the dictionary used for the contact picture.
        /// </summary>
        public string PictureTexture
        {
            get => pictureTexture;
            set
            {
                if (pictureTexture == value)
                {
                    return;
                }

                pictureTexture = value;
                OnChanged(PhoneInfoChanged.Picture);
            }
        }

        #endregion

        #region Functions

        private void OnChanged(PhoneInfoChanged info) => Changed?.Invoke(this, new PhoneContactChangedEventArgs(this, info));

        #endregion
    }
}
#endif
