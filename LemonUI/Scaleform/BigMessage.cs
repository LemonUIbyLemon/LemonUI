#if FIVEM
using CitizenFX.Core;
#elif RPH
using Rage;
#elif SHVDN2
using GTA.Native;
#elif SHVDN3
using GTA;
#endif
using System;

namespace LemonUI.Scaleform
{
    /// <summary>
    /// The type for a big message.
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// A centered message with customizable text an d background colors.
        /// Internally called SHOW_SHARD_CENTERED_MP_MESSAGE.
        /// </summary>
        Customizable = 0,
        /// <summary>
        /// Used when you rank up on GTA Online.
        /// Internally called SHOW_SHARD_CREW_RANKUP_MP_MESSAGE.
        /// </summary>
        RankUp = 1,
        /// <summary>
        /// The Mission Passed screen on PS3 and Xbox 360.
        /// Internally called SHOW_MISSION_PASSED_MESSAGE.
        /// </summary>
        MissionPassedOldGen = 2,
        /// <summary>
        /// The Message Type shown on the Wasted screen.
        /// Internally called SHOW_SHARD_WASTED_MP_MESSAGE.
        /// </summary>
        Wasted = 3,
        /// <summary>
        /// Used on the GTA Online Freemode event announcements.
        /// Internally called SHOW_PLANE_MESSAGE.
        /// </summary>
        Plane = 4,
        /// <summary>
        /// Development leftover from when GTA Online was Cops and Crooks.
        /// Internally called SHOW_BIG_MP_MESSAGE.
        /// </summary>
        CopsAndCrooks = 5,
        /// <summary>
        /// Message shown when the player purchases a weapon.
        /// Internally called SHOW_WEAPON_PURCHASED.
        /// </summary>
        Weapon = 6,
        /// <summary>
        /// Unknown where this one is used.
        /// Internally called SHOW_CENTERED_MP_MESSAGE_LARGE.
        /// </summary>
        CenteredLarge = 7,
    }

    /// <summary>
    /// A customizable big message.
    /// </summary>
    public class BigMessage : BaseScaleform
    {
        #region Constant Fields

        private const WeaponHash unarmed = (WeaponHash)2725352035;

        #endregion

        #region Private Fields

        /// <summary>
        /// The type of message to show.
        /// </summary>
        private MessageType type;

        #endregion

        #region Public Properties

        /// <summary>
        /// The title of the message.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The subtitle or description of the message.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// The color of the text.
        /// Only used on the Customizable message type.
        /// </summary>
        public int TextColor { get; set; }
        /// <summary>
        /// The color of the background.
        /// Only used on the Customizable message type.
        /// </summary>
        public int BackgroundColor { get; set; }
        /// <summary>
        /// The Rank on Cops and Crooks.
        /// </summary>
        public string Rank { get; set; }
        /// <summary>
        /// The hash of the Weapon.
        /// </summary>
        public WeaponHash Weapon { get; set; }
        /// <summary>
        /// The type of message to show.
        /// </summary>
        public MessageType Type
        {
            get => type;
            set
            {
                if (!Enum.IsDefined(typeof(MessageType), value))
                {
                    throw new InvalidOperationException($"{value} is not a valid message type.");
                }
                type = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a standard customizable message with just a title.
        /// </summary>
        /// <param name="title">The title to use.</param>
        public BigMessage(string title) : this(title, "", "", unarmed, 0, 0, MessageType.Customizable)
        {
        }

        /// <summary>
        /// Creates a custom message with the specified title.
        /// </summary>
        /// <param name="title">The title to use.</param>
        /// <param name="type">The type of message.</param>
        public BigMessage(string title, MessageType type) : this(title, "", "", unarmed, 0, 0, type)
        {
        }

        /// <summary>
        /// Creates a standard customizable message with a title and message.
        /// </summary>
        /// <param name="title">The title to use.</param>
        /// <param name="message">The message to show.</param>
        public BigMessage(string title, string message) : this(title, message, "", unarmed, 0, 0, MessageType.Customizable)
        {
        }

        /// <summary>
        /// Creates a Cops and Crooks message type.
        /// </summary>
        /// <param name="title">The title to use.</param>
        /// <param name="message">The message to show.</param>
        /// <param name="rank">Text to show in the Rank space.</param>
        public BigMessage(string title, string message, string rank) : this(title, message, rank, unarmed, 0, 0, MessageType.CopsAndCrooks)
        {
        }

        /// <summary>
        /// Creates a message with the specified type, title and message.
        /// </summary>
        /// <param name="title">The title to use.</param>
        /// <param name="message">The message to show.</param>
        /// <param name="type">The type of message.</param>
        public BigMessage(string title, string message, MessageType type) : this(title, message, "", unarmed, 0, 0, type)
        {
        }

        /// <summary>
        /// Creates a standard customizable message with a title and a custom text color.
        /// </summary>
        /// <param name="title">The title to use.</param>
        /// <param name="colorText">The color of the text.</param>
        public BigMessage(string title, int colorText) : this(title, "", "", unarmed, colorText, 0, MessageType.Customizable)
        {
        }

        /// <summary>
        /// Creates a standard customizable message with a specific title and custom colors.
        /// </summary>
        /// <param name="title">The title to use.</param>
        /// <param name="colorText">The color of the text.</param>
        /// <param name="colorBackground">The color of the background.</param>
        public BigMessage(string title, int colorText, int colorBackground) : this(title, "", "", unarmed, colorText, colorBackground, MessageType.Customizable)
        {
        }

        /// <summary>
        /// Creates a Weapon Purchase message with a custom text and weapons.
        /// </summary>
        /// <param name="title">The title to use.</param>
        /// <param name="weapon">The name of the Weapon.</param>
        /// <param name="hash">The hash of the Weapon image.</param>
        public BigMessage(string title, string weapon, WeaponHash hash) : this(title, "", weapon, hash, 0, 0, MessageType.Weapon)
        {
        }

        /// <summary>
        /// Creates a Weapon Purchase message with a custom text and weapons.
        /// </summary>
        /// <param name="title">The title to use.</param>
        /// <param name="message">The message to show.</param>
        /// <param name="weapon">The name of the Weapon.</param>
        /// <param name="hash">The hash of the Weapon image.</param>
        public BigMessage(string title, string message, string weapon, WeaponHash hash) : this(title, message, weapon, hash, 0, 0, MessageType.Weapon)
        {
        }

        /// <summary>
        /// Creates a message with all of the selected information.
        /// </summary>
        /// <param name="title">The title to use.</param>
        /// <param name="message">The message to show.</param>
        /// <param name="rank">The Rank on Cops and Crooks.</param>
        /// <param name="weapon">The hash of the Weapon image.</param>
        /// <param name="colorText">The color of the text.</param>
        /// <param name="colorBackground">The color of the background.</param>
        /// <param name="type">The type of message.</param>
        public BigMessage(string title, string message, string rank, WeaponHash weapon, int colorText, int colorBackground, MessageType type) : base("MP_BIG_MESSAGE_FREEMODE")
        {
            Title = title;
            Message = message;
            Rank = rank;
            Weapon = weapon;
            TextColor = colorText;
            BackgroundColor = colorBackground;
            Type = type;
            Update();
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Updates the Message information in the Scaleform.
        /// </summary>
        public override void Update()
        {
            // Select the correct function to call
            string function;
            switch (type)
            {
                case MessageType.Customizable:
                    function = "SHOW_SHARD_CENTERED_MP_MESSAGE";
                    break;
                case MessageType.RankUp:
                    function = "SHOW_SHARD_CREW_RANKUP_MP_MESSAGE";
                    break;
                case MessageType.MissionPassedOldGen:
                    function = "SHOW_MISSION_PASSED_MESSAGE";
                    break;
                case MessageType.Wasted:
                    function = "SHOW_SHARD_WASTED_MP_MESSAGE";
                    break;
                case MessageType.Plane:
                    function = "SHOW_PLANE_MESSAGE";
                    break;
                case MessageType.CopsAndCrooks:
                    function = "SHOW_BIG_MP_MESSAGE";
                    break;
                case MessageType.Weapon:
                    function = "SHOW_WEAPON_PURCHASED";
                    break;
                case MessageType.CenteredLarge:
                    function = "SHOW_CENTERED_MP_MESSAGE_LARGE";
                    break;
                default:
                    throw new InvalidOperationException($"{type} is not a valid message type.");
            }

            // And add the parameters
            switch (type)
            {
                case MessageType.Customizable:
                    CallFunction(function, Title, Message, TextColor, BackgroundColor);
                    break;
                case MessageType.CopsAndCrooks:
                    CallFunction(function, Title, Message, Rank);
                    break;
                case MessageType.Weapon:
                    CallFunction(function, Title, Message, (int)Weapon);
                    break;
                default:
                    CallFunction(function, Title, Message);
                    break;
            }
        }

        #endregion
    }
}
