#if ALTV
using AltV.Net.Client;
#elif FIVEM
using CitizenFX.Core;
using CitizenFX.Core.Native;
#elif RAGEMP
using RAGE.Game;
#elif RPH
using Rage;
using Rage.Native;
#elif SHVDN3 || SHVDNC
using GTA;
using GTA.Native;
#endif
using System;

namespace LemonUI.Scaleform
{
    /// <summary>
    /// The foreground of the celebration scaleform.
    /// </summary>
    public class Celebration : CelebrationCore
    {
        #region Fields

        private long showUntil = 0;
        private bool cancel = false;

        #endregion

        #region Properties

        /// <summary>
        /// The background of the scaleform.
        /// </summary>
        public CelebrationBackground Background { get; } = new CelebrationBackground();
        /// <summary>
        /// The foreground of the scaleform.
        /// </summary>
        public CelebrationForeground Foreground { get; } = new CelebrationForeground();
        /// <summary>
        /// The mode name shown.
        /// </summary>
        /// <remarks>
        /// This is used to show the current mode, like "HEIST", "RACE", "LAST TEAM STANDING", etc.
        /// </remarks>
        public string Mode { get; set; } = string.Empty;
        /// <summary>
        /// The job name shown.
        /// </summary>
        /// <remarks>
        /// This is used to show the current job name like "The Prison Break" or "Premium Race - East Coast".
        /// </remarks>
        public string Job { get; set; } = string.Empty;
        /// <summary>
        /// The challenge shown.
        /// </summary>
        /// <remarks>
        /// This is used to show messages like "ROUND 10" or "POTENTIAL CUT $1000000".
        /// </remarks>
        public string Challenge { get; set; } = string.Empty;
        /// <summary>
        /// For how long the scalefom is show.
        /// </summary>
        public int Duration { get; set; } = 2;
        /// <summary>
        /// The style of the celebration.
        /// </summary>
        public CelebrationStyle Style { get; set; } = CelebrationStyle.Clean;

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when the scaleform is shown on the screen.
        /// </summary>
        public event EventHandler Shown;
        /// <summary>
        /// Event triggered when the scaleform has finished fading out.
        /// </summary>
        public event EventHandler Finished;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new Celebration scaleform.
        /// </summary>
        public Celebration() : base("MP_CELEBRATION")
        {
        }

        #endregion

        #region Tools

        private void CallFunctionOnAll(string function, params object[] parameters)
        {
            Background.CallFunction(function, parameters);
            Foreground.CallFunction(function, parameters);
            CallFunction(function, parameters);
        }

        #endregion

        #region Functions

        /// <summary>
        /// Shows the celebration scaleform.
        /// </summary>
        public void Show()
        {
            cancel = false;
            Visible = true;

            const string wallId = "intro";

            CallFunctionOnAll("CLEANUP", wallId);
            CallFunctionOnAll("CREATE_STAT_WALL", wallId, "HUD_COLOUR_BLACK", 40);

            CallFunctionOnAll("SET_PAUSE_DURATION", Duration);
            // challengeTextLabel appears to be used as a bool and challengePartsText is appended
            CallFunctionOnAll("ADD_INTRO_TO_WALL", wallId, Mode, Job, true, Challenge, string.Empty, Challenge, Duration, string.Empty, true, "HUD_COLOUR_WHITE");
            CallFunctionOnAll("ADD_BACKGROUND_TO_WALL", wallId, 75, (int)Style);
            CallFunctionOnAll("SHOW_STAT_WALL", wallId);

            Shown?.Invoke(this, EventArgs.Empty);

            // Explanation for future me
            // 333ms going in
            // 333ms going out
#if ALTV
            int time = Alt.Natives.GetGameTimer();
#elif RAGEMP
            int time = Misc.GetGameTimer();
#elif RPH
            uint time = Game.GameTime;
#elif FIVEM || SHVDN3 || SHVDNC
            int time = Game.GameTime;
#endif
            showUntil = time + 333 + 333 + (Duration * 1000);
        }
        /// <summary>
        /// Cancels the current screen being shown.
        /// </summary>
        public void Cancel()
        {
            if (Visible)
            {
                cancel = true;
            }
        }
        /// <inheritdoc/>
        public override void Process()
        {
            if (!Visible)
            {
                return;
            }

            if (cancel)
            {
                showUntil = -1;
                Visible = false;
                return;
            }

            DrawFullScreen();

#if ALTV
            int time = Alt.Natives.GetGameTimer();
#elif RAGEMP
            int time = Misc.GetGameTimer();
#elif RPH
            uint time = Game.GameTime;
#elif FIVEM || SHVDN3 || SHVDNC
            int time = Game.GameTime;
#endif

            if (showUntil < time)
            {
                showUntil = 0;
                Visible = false;
                Finished?.Invoke(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// Draws the celebration scaleform.
        /// </summary>
        public override void DrawFullScreen()
        {
            if (!Visible)
            {
                return;
            }

            #if ALTV
            Alt.Natives.DrawScaleformMovieFullscreenMasked(Background.Handle, Foreground.Handle, 255, 255, 255, 255);
            Alt.Natives.DrawScaleformMovieFullscreen(Handle, 255, 255, 255, 255, 255);
            #elif FIVEM
            API.DrawScaleformMovieFullscreenMasked(Background.Handle, Foreground.Handle, 255, 255, 255, 255);
            API.DrawScaleformMovieFullscreen(Handle, 255, 255, 255, 255, 255);
            #elif RAGEMP
            Invoker.Invoke(Natives.DrawScaleformMovieFullscreenMasked, Background.Handle, Foreground.Handle, 255, 255, 255, 255);
            Invoker.Invoke(Natives.DrawScaleformMovieFullscreen, Handle, 255, 255, 255, 255);
            #elif RPH
            NativeFunction.CallByHash<int>(0xCF537FDE4FBD4CE5, Background.Handle, Foreground.Handle, 255, 255, 255, 255);
            NativeFunction.CallByHash<int>(0x0DF606929C105BE1, Handle, 255, 255, 255, 255);
            #elif SHVDN3
            Function.Call(Hash.DRAW_SCALEFORM_MOVIE_FULLSCREEN_MASKED, Background.Handle, Foreground.Handle, 255, 255, 255, 255);
            Function.Call(Hash.DRAW_SCALEFORM_MOVIE_FULLSCREEN, Handle, 255, 255, 255, 255);
            #endif
        }

        #endregion
    }
}
