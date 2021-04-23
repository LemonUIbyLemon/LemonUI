#if FIVEM
using CitizenFX.Core;
using CitizenFX.Core.Native;
#elif RPH
using Rage;
using Rage.Native;
using Control = Rage.GameControl;
#elif SHVDN2
using GTA;
using GTA.Native;
#elif SHVDN3
using GTA;
using GTA.Native;
#endif
using System;
using System.Collections.Generic;

namespace LemonUI.Scaleform
{
    /// <summary>
    /// The Background of the BruteForce Hack Minigame.
    /// </summary>
    public enum BruteForceBackground
    {
        /// <summary>
        /// A simple Black background.
        /// </summary>
        Black = 0,
        /// <summary>
        /// A simple Purple background.
        /// </summary>
        Purple = 1,
        /// <summary>
        /// A simple Gray background.
        /// </summary>
        Gray = 2,
        /// <summary>
        /// A simple Light Blue background.
        /// </summary>
        LightBlue = 3,
        /// <summary>
        /// A Light Blue Wallpaper.
        /// </summary>
        Wallpaper1 = 4,
        /// <summary>
        /// A Fade from Gray in the center to Black in the corners.
        /// </summary>
        DarkFade = 5,
    }

    /// <summary>
    /// The status of the BruteForce Hack after finishing.
    /// </summary>
    public enum BruteForceStatus
    {
        /// <summary>
        /// The user completed the hack successfully.
        /// </summary>
        Completed = 0,
        /// <summary>
        /// The user ran out of time.
        /// </summary>
        OutOfTime = 1,
        /// <summary>
        /// The player ran out of lives.
        /// </summary>
        OutOfLives = 2,
    }

    /// <summary>
    /// Event information after an the BruteForce hack has finished.
    /// </summary>
    public class BruteForceFinishedEventArgs
    {
        /// <summary>
        /// The final status of the Hack.
        /// </summary>
        public BruteForceStatus Status { get; }

        internal BruteForceFinishedEventArgs(BruteForceStatus status)
        {
            Status = status;
        }
    }

    /// <summary>
    /// Represents the method that is called when the end user finishes the BruteForce hack.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An <see cref="BruteForceFinishedEventArgs"/> with the hack status.</param>
    public delegate void BruteForceFinishedEventHandler(object sender, BruteForceFinishedEventArgs e);

    /// <summary>
    /// The BruteForce Hacking Minigame shown in multiple missions.
    /// </summary>
    public class BruteForce : BaseScaleform
    {
        #region Fields

        private static readonly Random random = new Random();
        private static readonly Sound soundRowSwitch = new Sound("", "HACKING_MOVE_CURSOR");
        private static readonly Sound soundRowCompleted = new Sound("", "HACKING_CLICK");
        private static readonly Sound soundRowFailed = new Sound("", "HACKING_CLICK_BAD");
        private static readonly Sound soundSuccess = new Sound("", "HACKING_SUCCESS");

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
                    throw new ArgumentOutOfRangeException("The word needs to be exactly 8 characters long.", nameof(value));
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
        /// After how many miliseconds should the Hack close automatically.
        /// Set to -1 to keep the Hack window open.
        /// </summary>
        public int CloseAfter
        {
            get => closeAfter;
            set
            {
                if (value < -1)
                {
                    throw new ArgumentOutOfRangeException("The Closure time can't be under -1.", nameof(value));
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

            end = TimeSpan.FromMilliseconds(Game.GameTime) + countdown;
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
                throw new ArgumentOutOfRangeException("The index needs to be between 0 and 7.", nameof(index));
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
            // If there is a time set to hide the Hack window
            if (hideTime != -1)
            {
                // If that time has already passed, go ahead and hide the window
                if (hideTime <= Game.GameTime)
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
                    TimeSpan span = countdown - (TimeSpan.FromMilliseconds(Game.GameTime) - end);

                    // If is lower or equal than zero, the player failed
                    if (span <= TimeSpan.Zero)
                    {
                        CallFunction("SET_COUNTDOWN", 0, 0, 0);
                        string err = FailMessages.Count == 0 ? "" : FailMessages[random.Next(FailMessages.Count)];
                        CallFunction("SET_ROULETTE_OUTCOME", false, err);
                        hideTime = closeAfter == -1 ? -1 : (int)Game.GameTime + CloseAfter;
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
                            case 86:  // Hack Completed
                                string ok = SuccessMessages.Count == 0 ? "" : SuccessMessages[random.Next(SuccessMessages.Count)];
                                CallFunction("SET_ROULETTE_OUTCOME", true, ok);
                                soundSuccess.PlayFrontend();
                                HackFinished?.Invoke(this, new BruteForceFinishedEventArgs(BruteForceStatus.Completed));
                                hideTime = closeAfter == -1 ? -1 : (int)Game.GameTime + CloseAfter;
                                inProgress = false;
                                break;
                            case 87:  // Row Failed (or lives failed)
                                livesCurrent--;
                                CallFunction("SET_LIVES", livesCurrent, livesTotal);
                                soundRowFailed.PlayFrontend();
                                if (livesCurrent <= 0)
                                {
                                    string err = FailMessages.Count == 0 ? "" : FailMessages[random.Next(FailMessages.Count)];
                                    CallFunction("SET_ROULETTE_OUTCOME", false, err);
                                    hideTime = closeAfter == -1 ? -1 : (int)Game.GameTime + CloseAfter;
                                    inProgress = false;
                                    HackFinished?.Invoke(this, new BruteForceFinishedEventArgs(BruteForceStatus.OutOfLives));
                                }
                                break;
                            case 92:  // Row Completed
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
