#if FIVEMV2
using CitizenFX.FiveM;
#elif FIVEM
using CitizenFX.Core;
#elif RAGEMP
using RAGE.Game;
#elif RPH
using Rage;
#elif SHVDN3 || SHVDNC
using GTA;
#elif ALTV
using AltV.Net.Client;
#endif
using System;

namespace LemonUI.Scaleform
{
    /// <summary>
    /// A customizable big message.
    /// </summary>
    public class BigMessage : BaseScaleform
    {
        #region Constants

        private const uint unarmed = 0xA2719263;

        #endregion

        #region Fields

        private MessageType type;
        private uint weaponHash;
        private long hideAfter;
        private string title;
        private string message;
        private string rank;

        #endregion

        #region Properties

        /// <summary>
        /// The title of the message.
        /// </summary>
        public string Title
        {
            get => title;
            set => title = value ?? throw new ArgumentNullException(nameof(value));
        }
        /// <summary>
        /// The subtitle or description of the message.
        /// </summary>
        public string Message
        {
            get => message;
            set => message = value ?? throw new ArgumentNullException(nameof(value));
        }
        /// <summary>
        /// The color of the text.
        /// Only used on the Customizable message type.
        /// </summary>
        public int TextColor { get; set; }
        /// <summary>
        /// The color of the background in the Customizable message type.
        /// </summary>
        public int BackgroundColor { get; set; }
        /// <summary>
        /// The Rank when the mode is set to Cops and Crooks.
        /// </summary>
        public string Rank
        {
            get => rank;
            set => rank = value ?? throw new ArgumentNullException(nameof(value));
        }
#if !RAGEMP && !ALTV
        /// <summary>
        /// The hash of the Weapon as an enum.
        /// </summary>
        public WeaponHash Weapon
        {
            get => (WeaponHash)weaponHash;
            set => weaponHash = (uint)value;
        }
#endif
        /// <summary>
        /// The hash of the Weapon as it's native value.
        /// </summary>
        public uint WeaponHash
        {
            get => weaponHash;
            set => weaponHash = value;
        }
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
        public BigMessage(string title) : this(title, string.Empty, string.Empty, unarmed, 0, 0, MessageType.Customizable)
        {
        }
        /// <summary>
        /// Creates a custom message with the specified title.
        /// </summary>
        /// <param name="title">The title to use.</param>
        /// <param name="type">The type of message.</param>
        public BigMessage(string title, MessageType type) : this(title, string.Empty, string.Empty, unarmed, 0, 0, type)
        {
        }
        /// <summary>
        /// Creates a standard customizable message with a title and message.
        /// </summary>
        /// <param name="title">The title to use.</param>
        /// <param name="message">The message to show.</param>
        public BigMessage(string title, string message) : this(title, message, string.Empty, unarmed, 0, 0, MessageType.Customizable)
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
        public BigMessage(string title, string message, MessageType type) : this(title, message, string.Empty, unarmed, 0, 0, type)
        {
        }
        /// <summary>
        /// Creates a standard customizable message with a title and a custom text color.
        /// </summary>
        /// <param name="title">The title to use.</param>
        /// <param name="colorText">The color of the text.</param>
        public BigMessage(string title, int colorText) : this(title, string.Empty, string.Empty, unarmed, colorText, 0, MessageType.Customizable)
        {
        }
        /// <summary>
        /// Creates a standard customizable message with a specific title and custom colors.
        /// </summary>
        /// <param name="title">The title to use.</param>
        /// <param name="colorText">The color of the text.</param>
        /// <param name="colorBackground">The color of the background.</param>
        public BigMessage(string title, int colorText, int colorBackground) : this(title, string.Empty, string.Empty, unarmed, colorText, colorBackground, MessageType.Customizable)
        {
        }
#if !RAGEMP && !ALTV
        /// <summary>
        /// Creates a Weapon Purchase message with a custom text and weapons.
        /// </summary>
        /// <param name="title">The title to use.</param>
        /// <param name="weapon">The name of the Weapon.</param>
        /// <param name="hash">The hash of the Weapon image.</param>
        public BigMessage(string title, string weapon, WeaponHash hash) : this(title, string.Empty, weapon, hash, 0, 0, MessageType.Weapon)
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
        public BigMessage(string title, string message, string rank, WeaponHash weapon, int colorText, int colorBackground, MessageType type) : this(title, message, rank, (uint)weapon, colorText, colorBackground, type)
        {
        }
#endif
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
        public BigMessage(string title, string message, string rank, uint weapon, int colorText, int colorBackground, MessageType type) : base("MP_BIG_MESSAGE_FREEMODE")
        {
            Title = title;
            Message = message;
            Rank = rank;
            WeaponHash = weapon;
            TextColor = colorText;
            BackgroundColor = colorBackground;
            Type = type;
            Update();
        }

        #endregion

        #region Functions

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
                    CallFunction(function, Title, Message, (int)weaponHash);
                    break;
                default:
                    CallFunction(function, Title, Message);
                    break;
            }
        }
        /// <summary>
        /// Fades the big message out.
        /// </summary>
        /// <param name="time">The time it will take to do the fade.</param>
        public void FadeOut(int time)
        {
            if (time < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(time), "Time can't be under zero.");
            }

            CallFunction("SHARD_ANIM_OUT", 0, time);

#if ALTV
            long currentTime = Alt.Natives.GetGameTimer();
#elif RAGEMP
            long currentTime = Misc.GetGameTimer();
#elif FIVEM || RPH || SHVDN3 || SHVDNC || FIVEMV2
            long currentTime = Game.GameTime;
#endif
            hideAfter = currentTime + time;
        }
        /// <inheritdoc/>
        public override void DrawFullScreen()
        {
#if ALTV
            long time = Alt.Natives.GetGameTimer();
#elif RAGEMP
            long time = Misc.GetGameTimer();
#elif FIVEM || RPH || SHVDN3 || SHVDNC || FIVEMV2
            long time = Game.GameTime;
#endif

            if (hideAfter > 0 && time > hideAfter)
            {
                Visible = false;
                hideAfter = 0;
            }

            base.DrawFullScreen();
        }

        #endregion
    }
}
