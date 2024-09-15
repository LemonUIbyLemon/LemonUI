#if FIVEMV2
using CitizenFX.FiveM;
#elif ALTV
using AltV.Net.Client;
#elif FIVEM
using CitizenFX.Core;
#elif RAGEMP
using RAGE.Game;
#elif RPH
using Rage;
#elif SHVDN3 || SHVDNC
using GTA;
#endif
using System;

namespace LemonUI.Scaleform
{
    /// <summary>
    /// The Countdown scaleform in the GTA Online races.
    /// </summary>
    public class Countdown : BaseScaleform
    {
        #region Fields

        private int duration = 3;
        private long lastStepTime = 0;

        #endregion

        #region Defaults

        // TODO: Add the Stunt Race sounds, Countdown_1 and Countdown_GO in DLC_AW_Frontend_Sounds
        // TODO: See why Countdown_GO doesn't plays correctly

        /// <summary>
        /// The default sound played when the countdown
        /// </summary>
        public static Sound DefaultCountSound = new Sound("HUD_MINI_GAME_SOUNDSET", "CHECKPOINT_NORMAL");
        /// <summary>
        /// The default sound when th
        /// </summary>
        public static Sound DefaultGoSound = new Sound("HUD_MINI_GAME_SOUNDSET", "GO");

        #endregion

        #region Properties

        /// <summary>
        /// The sound played when counting down.
        /// </summary>
        public Sound CountSound { get; set; } = DefaultCountSound;
        /// <summary>
        /// The sound played when the countdown has finished.
        /// </summary>
        public Sound GoSound { get; set; } = DefaultGoSound;
        /// <summary>
        /// The duration of the countdown.
        /// </summary>
        public int Duration
        {
            get => duration;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The duration can't be equal or under zero.");
                }

                duration = value;
            }
        }
        /// <summary>
        /// The current count.
        /// </summary>
        public int Current { get; private set; }

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when the countdown starts.
        /// </summary>
        public event EventHandler Started;
        /// <summary>
        /// Event triggered when the countdown has finished.
        /// </summary>
        public event EventHandler Finished;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new countdown scaleform.
        /// </summary>
        public Countdown() : base("COUNTDOWN")
        {
        }

        #endregion

        #region Tools

        private void ShowStep(int step)
        {
            string asString = step == 0 ? "GO" : step.ToString();

            CallFunction("SET_MESSAGE", asString, 255, 255, 255, true);
            CallFunction("FADE_MP", asString, 255, 255, 255);

            if (step == 0)
            {
                GoSound?.PlayFrontend();
            }
            else
            {
                CountSound?.PlayFrontend();
            }
        }

        #endregion

        #region Functions

        /// <summary>
        /// Starts the countdown.
        /// </summary>
        public void Start()
        {
            Visible = true;

#if ALTV
            lastStepTime = Alt.Natives.GetGameTimer();
#elif RAGEMP
            lastStepTime = Misc.GetGameTimer();
#elif FIVEM || RPH || SHVDN3 || SHVDNC || FIVEMV2
            lastStepTime = Game.GameTime;
#endif

            Current = duration;
            ShowStep(Current);
            Started?.Invoke(this, EventArgs.Empty);
        }
        /// <inheritdoc/>
        public override void Process()
        {
            if (!Visible)
            {
                return;
            }

            if (lastStepTime > 0)
            {
#if ALTV
                long currentTime = Alt.Natives.GetGameTimer();
#elif RAGEMP
                long currentTime = Misc.GetGameTimer();
#elif FIVEM || RPH || SHVDN3 || SHVDNC || FIVEMV2
                long currentTime = Game.GameTime;
#endif

                if (currentTime - lastStepTime >= 1000)
                {
                    if (Current == 0)
                    {
                        lastStepTime = 0;
                        Visible = false;
                        return;
                    }

                    lastStepTime = currentTime;
                    Current--;
                    ShowStep(Current);

                    if (Current == 0)
                    {
                        Finished?.Invoke(this, EventArgs.Empty);
                    }
                }
            }

            DrawFullScreen();
        }
        /// <inheritdoc/>
        public override void Update()
        {
        }

        #endregion
    }
}
