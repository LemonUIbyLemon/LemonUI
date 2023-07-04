#if ALTV
using AltV.Net.Client;
#elif FIVEM
using CitizenFX.Core.Native;
#elif RAGEMP
using RAGE.Game;
#elif RPH
using Rage.Native;
#elif SHVDN3
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
        public int Duration { get; set; } = 5;
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
            const string wallId = "intro";

            CallFunctionOnAll("CLEANUP", wallId);
            CallFunctionOnAll("CREATE_STAT_WALL", wallId, "HUD_COLOUR_BLACK", 40);

            CallFunctionOnAll("SET_PAUSE_DURATION", Duration);
            // challengeTextLabel appears to be used as a bool and challengePartsText is appended
            CallFunctionOnAll("ADD_INTRO_TO_WALL", wallId, Mode, Job, true, Challenge, string.Empty, Challenge, Duration, string.Empty, true, "HUD_COLOUR_WHITE");
            CallFunctionOnAll("ADD_BACKGROUND_TO_WALL", wallId, 75, (int)Style);
            CallFunctionOnAll("SHOW_STAT_WALL", wallId);

            Visible = true;
            Shown?.Invoke(this, EventArgs.Empty);
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
