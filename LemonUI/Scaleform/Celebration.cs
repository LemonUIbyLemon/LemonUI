#if SHVDN3
using GTA.Native;
#endif

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
        /// The title of the scaleform.
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// The subtitle of the scaleform.
        /// </summary>
        public string Subtitle { get; set; } = string.Empty;
        /// <summary>
        /// For how long the scalefom is show.
        /// </summary>
        public int Duration { get; set; } = 5;

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
            CallFunctionOnAll("CREATE_STAT_WALL", wallId, "HUD_COLOR_BLACK");

            CallFunctionOnAll("SET_PAUSE_DURATION", Duration);
            CallFunctionOnAll("ADD_INTRO_TO_WALL", wallId, Title, Subtitle, "", "", "", 0, 0, 0, true, "HUD_COLOUR_WHITE");
            CallFunctionOnAll("ADD_BACKGROUND_TO_WALL", wallId, 75, 0);
            CallFunctionOnAll("SHOW_STAT_WALL", wallId);
            Visible = true;
        }
        /// <summary>
        /// Draws the celebration scaleform.
        /// </summary>
        public override void DrawFullScreen()
        {
            #if SHVDN3
            Function.Call(Hash.DRAW_SCALEFORM_MOVIE_FULLSCREEN_MASKED, Background.Handle, Foreground.Handle, 255, 255, 255, 255);
            Function.Call(Hash.DRAW_SCALEFORM_MOVIE_FULLSCREEN, Handle, 255, 255, 255, 255);
            #endif
        }

        #endregion
    }
}
