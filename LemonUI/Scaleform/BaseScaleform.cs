#if FIVEM
using CitizenFX.Core.Native;
#else
using GTA.Native;
#endif

namespace LemonUI.Scaleform
{
    /// <summary>
    /// Represents a generic Scaleform object.
    /// </summary>
    public abstract class BaseScaleform : IScaleform
    {
        #region Private Fields

        /// <summary>
        /// The ID of the scaleform.
        /// </summary>
#if FIVEM
        protected CitizenFX.Core.Scaleform scaleform = null;
#else
        protected GTA.Scaleform scaleform = null;
#endif

        #endregion

        #region Public Properties

        /// <summary>
        /// If the Scaleform should be visible or not.
        /// </summary>
        public bool Visible { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new Scaleform class with the specified Scaleform object name.
        /// </summary>
        /// <param name="sc">The Scalform object.</param>
        public BaseScaleform(string sc)
        {
#if FIVEM
            scaleform = new CitizenFX.Core.Scaleform(sc);
#else
            scaleform = new GTA.Scaleform(sc);
#endif
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Draws the scaleform full screen.
        /// </summary>
        public void DrawFullScreen()
        {
            if (!Visible)
            {
                return;
            }
#if FIVEM
            API.DrawScaleformMovieFullscreen(scaleform.Handle, 255, 255, 255, 255, 0);
#else
            Function.Call(Hash.DRAW_SCALEFORM_MOVIE_FULLSCREEN, scaleform.Handle, 255, 255, 255, 255, 0);
#endif
        }
        /// <summary>
        /// Draws the scaleform full screen.
        /// </summary>
        public void Draw() => DrawFullScreen();
        /// <summary>
        /// Draws the scaleform full screen.
        /// </summary>
        public void Process() => DrawFullScreen();
        /// <summary>
        /// Marks the scaleform as no longer needed.
        /// </summary>
        public void Dispose()
        {
            int id = scaleform.Handle;
#if FIVEM
            API.SetScaleformMovieAsNoLongerNeeded(ref id);
#else
            Function.Call(Hash.SET_SCALEFORM_MOVIE_AS_NO_LONGER_NEEDED, new OutputArgument(id));
#endif
        }

        #endregion
    }
}
