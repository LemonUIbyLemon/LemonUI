#if ALTV
using AltV.Net.Client;
#elif FIVEM
using CitizenFX.Core;
#elif RAGEMP
using RAGE.Game;
#elif RPH
using Rage;
#elif SHVDN3
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
        private int currentStep = 0;

        #endregion

        #region Defaults

        /// <summary>
        /// The default sound played when the countdown
        /// </summary>
        public static Sound DefaultCountSound = new Sound("HUD_MINI_GAME_SOUNDSET", "CHECKPOINT_NORMAL");

        #endregion

        #region Properties

        /// <summary>
        /// The sound played when counting down.
        /// </summary>
        public Sound CountSound { get; set; } = DefaultCountSound;
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

            CountSound?.PlayFrontend();
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
#elif RPH
            lastStepTime = Game.GameTime;
#elif FIVEM || SHVDN3
            lastStepTime = Game.GameTime;
#endif

            currentStep = duration;
            ShowStep(currentStep);
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
                int currentTime = Alt.Natives.GetGameTimer();
#elif RAGEMP
                int currentTime = Misc.GetGameTimer();
#elif RPH
                uint currentTime = Game.GameTime;
#elif FIVEM || SHVDN3
                int currentTime = Game.GameTime;
#endif

                if (currentTime - lastStepTime >= 1000)
                {
                    if (currentStep == 0)
                    {
                        lastStepTime = 0;
                        Visible = false;
                        return;
                    }

                    lastStepTime = currentTime;
                    currentStep--;
                    ShowStep(currentStep);

                    if (currentStep == 0)
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
