#if FIVEM
using CitizenFX.Core;
using CitizenFX.Core.Native;
#elif RAGEMP
using RAGE.Game;
#elif RPH
using Rage;
using Rage.Native;
using Control = Rage.GameControl;
#elif SHVDN3
using GTA;
using GTA.Native;
#endif
using System;
using System.Collections.Generic;

namespace LemonUI.Scaleform
{
    /// <summary>
    /// The BruteForce Hacking Minigame shown in multiple missions.
    /// </summary>
    public class BruteForce : BaseScaleform
    {
        #region Fields

        private static readonly Random random = new Random();
        private static readonly Sound soundRowSwitch = new Sound(string.Empty, "HACKING_MOVE_CURSOR");
        private static readonly Sound soundRowCompleted = new Sound(string.Empty, "HACKING_CLICK");
        private static readonly Sound soundRowFailed = new Sound(string.Empty, "HACKING_CLICK_BAD");
        private static readonly Sound soundSuccess = new Sound(string.Empty, "HACKING_SUCCESS");

        private int hideTime = -1;
        private int output = 0;
        private bool firstRun = true;
        private bool inProgress = false;

        private BruteForceBackground background = BruteForceBackground.Black;
        private string word = "LEMONADE";
        private int livesTotal = 5;
        private int livesCurrent = 5;
        private int closeAfter = -1;
        private TimeSpan end = TimeSpan.Zero;
        private TimeSpan countdown = TimeSpan.Zero;
        private bool showLives = true;

        #endregion

        #region Properties

        /// <summary>
        /// The Word shown to select in the menu.
        /// </summary>
        public string Word
        {
            get => word;
            set
            {
                if (value.Length != 8)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The word needs to be exactly 8 characters long.");
                }
                word = value;
                CallFunction("SET_ROULETTE_WORD", value);
            }
        }
        /// <summary>
        /// The background of the Hacking minigame.
        /// </summary>
        public BruteForceBackground Background
        {
            get => background;
            set
            {
                background = value;
                CallFunction("SET_BACKGROUND", (int)value);
            }
        }
        /// <summary>
        /// The number of Lives of the minigame.
        /// </summary>
        public int TotalLives
        {
            get => livesTotal;
            set
            {
                livesTotal = value;
                if (livesCurrent > value)
                {
                    livesCurrent = value;
                }
                CallFunction("SET_LIVES", livesCurrent, value);
            }
        }
        /// <summary>
        /// The current number of lives that the player has.
        /// </summary>
        public int CurrentLives => livesCurrent;
        /// <summary>
        /// The messages that might appear on success.
        /// </summary>
        public List<string> SuccessMessages { get; } = new List<string>();
        /// <summary>
        /// The messages that will appear when the player fails.
        /// </summary>
        public List<string> FailMessages { get; } = new List<string>();
        /// <summary>
        /// The time in milliseconds to wait before closing the Hack window automatically.
        /// </summary>
        /// <remarks>
        /// This can be set to -1 to keep the Hack window open.
        /// </remarks>
        public int CloseAfter
        {
            get => closeAfter;
            set
            {
                if (value < -1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The Closure time can't be under -1.");
                }
                closeAfter = value;
            }
        }
        /// <summary>
        /// If the player can retry the hack after failing.
        /// </summary>
        public bool CanRetry { get; set; } = false;
        /// <summary>
        /// The countdown of the Hack minigame.
        /// </summary>
        public TimeSpan Countdown
        {
            get => countdown;
            set => countdown = value;
        }
        /// <summary>
        /// If the lives of the player should be shown on the top right.
        /// </summary>
        public bool ShowLives
        {
            get => showLives;
            set
            {
                showLives = value;
                CallFunction("SHOW_LIVES", value);
            }
        }
        /// <summary>
        /// If all of the rows should be restarted after the player fails one.
        /// </summary>
        public bool ResetOnRowFail { get; set; } = true;

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when the player finishes a hack.
        /// </summary>
        public event BruteForceFinishedEventHandler HackFinished;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new Hacking Scaleform.
        /// </summary>
        public BruteForce() : base("HACKING_PC")
        {
            Visible = false;
            for (int i = 0; i < 8; i++)
            {
                SetColumnSpeed(i, 100);
            }
        }

        #endregion

        #region Functions

        /// <summary>
        /// Resets the entire Hacking minigame.
        /// </summary>
        public void Reset()
        {
            inProgress = true;

            Background = background;
            RunProgram(4);
            RunProgram(83);
            TotalLives = livesTotal;
            Word = word;
            ShowLives = showLives;

#if RAGEMP
            int time = Misc.GetGameTimer();
#elif RPH
            uint time = Game.GameTime;
#else
            int time = Game.GameTime;
#endif

            end = TimeSpan.FromMilliseconds(time) + countdown;
        }
        /// <summary>
        /// Sets the speed of one of the 8 columns.
        /// </summary>
        /// <param name="index">The index of the column.</param>
        /// <param name="speed">The speed of the column.</param>
        public void SetColumnSpeed(int index, float speed)
        {
            if (index >= 8 || index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "The index needs to be between 0 and 7.");
            }
            CallFunction("SET_COLUMN_SPEED", index, speed);
        }
        /// <summary>
        /// Runs the specified Hacking program.
        /// </summary>
        /// <param name="program">The program to open.</param>
        public void RunProgram(int program)
        {
            CallFunction("RUN_PROGRAM", program);
        }
        /// <summary>
        /// Updates the information of the Hacking window.
        /// </summary>
        public override void Update()
        {
#if RAGEMP
            int time = Misc.GetGameTimer();
#elif RPH
            uint time = Game.GameTime;
#else
            int time = Game.GameTime;
#endif

            // If there is a time set to hide the Hack window
            if (hideTime != -1)
            {
                // If that time has already passed, go ahead and hide the window
                if (hideTime <= time)
                {
                    Visible = false;
                    hideTime = -1;
                    return;
                }
            }

            // If this is the first run and is not in progress, reset it
            if (firstRun && !inProgress)
            {
                firstRun = false;
                Reset();
            }

            // If the hack minigame is not in progress but the player can retry and he pressed enter, reset it
            if (!inProgress && CanRetry && Controls.IsJustPressed(Control.FrontendAccept))
            {
                Reset();
                hideTime = -1;
            }

            // If the Hack minigame is in progress
            if (inProgress)
            {
                // If there is a countdown set
                if (countdown > TimeSpan.Zero)
                {
                    // Calculate the time left
                    TimeSpan span = countdown - (TimeSpan.FromMilliseconds(time) - end);

                    // If is lower or equal than zero, the player failed
                    if (span <= TimeSpan.Zero)
                    {
                        CallFunction("SET_COUNTDOWN", 0, 0, 0);
                        string err = FailMessages.Count == 0 ? string.Empty : FailMessages[random.Next(FailMessages.Count)];
                        CallFunction("SET_ROULETTE_OUTCOME", false, err);
                        hideTime = closeAfter == -1 ? -1 : (int)time + CloseAfter;
                        inProgress = false;
                        HackFinished?.Invoke(this, new BruteForceFinishedEventArgs(BruteForceStatus.OutOfTime));
                        return;
                    }
                    // Otherwise, update the visible time
                    else
                    {
                        CallFunction("SET_COUNTDOWN", span.Minutes, span.Seconds, span.Milliseconds);
                    }
                }

                // If the user pressed left, go to the left
                if (Controls.IsJustPressed(Control.MoveLeftOnly) || Controls.IsJustPressed(Control.FrontendLeft))
                {
                    soundRowSwitch.PlayFrontend();
                    CallFunction("SET_INPUT_EVENT", 10);
                }
                // If the user pressed right, go to the right
                else if (Controls.IsJustPressed(Control.MoveRightOnly) || Controls.IsJustPressed(Control.FrontendRight))
                {
                    soundRowSwitch.PlayFrontend();
                    CallFunction("SET_INPUT_EVENT", 11);
                }
                // If the user pressed accept, send the selection event
                else if (Controls.IsJustPressed(Control.FrontendAccept))
                {
                    output = CallFunctionReturn("SET_INPUT_EVENT_SELECT");
                }

                // If there is some output to receive
                if (output != 0)
                {
                    // If the value is ready, go ahead and check it
                    if (IsValueReady(output))
                    {
                        switch (GetValue<int>(output))
                        {
                            case 86: // Hack Completed
                                string ok = SuccessMessages.Count == 0 ? string.Empty : SuccessMessages[random.Next(SuccessMessages.Count)];
                                CallFunction("SET_ROULETTE_OUTCOME", true, ok);
                                soundSuccess.PlayFrontend();
                                HackFinished?.Invoke(this, new BruteForceFinishedEventArgs(BruteForceStatus.Completed));
                                hideTime = closeAfter == -1 ? -1 : (int)time + CloseAfter;
                                inProgress = false;
                                break;
                            case 87: // Row Failed (or lives failed)
                                livesCurrent--;
                                CallFunction("SET_LIVES", livesCurrent, livesTotal);
                                soundRowFailed.PlayFrontend();
                                if (livesCurrent <= 0)
                                {
                                    string err = FailMessages.Count == 0 ? string.Empty : FailMessages[random.Next(FailMessages.Count)];
                                    CallFunction("SET_ROULETTE_OUTCOME", false, err);
                                    hideTime = closeAfter == -1 ? -1 : (int)time + CloseAfter;
                                    inProgress = false;
                                    HackFinished?.Invoke(this, new BruteForceFinishedEventArgs(BruteForceStatus.OutOfLives));
                                }
                                break;
                            case 92: // Row Completed
                                soundRowCompleted.PlayFrontend();
                                break;
                        }
                        output = 0;
                    }
                }
            }
        }

        #endregion
    }
}
