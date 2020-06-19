#if FIVEM
using CitizenFX.Core.Native;
#else
using GTA.Native;
#endif

namespace LemonUI.Scaleform
{
    /// <summary>
    /// Loading screen like the transition between story mode and online.
    /// </summary>
    public class LoadingScreen : BaseScaleform
    {
        #region Natives

#if !FIVEM
        private static readonly Hash BEGIN_SCALEFORM_MOVIE_METHOD = (Hash)0xF6E48914C7A8694E;
        private static readonly Hash END_SCALEFORM_MOVIE_METHOD = (Hash)0xC6796A8FFA375E53;
        private static readonly Hash CALL_SCALEFORM_MOVIE_METHOD = (Hash)0xFBD96D87AC96D533;
        private static readonly Hash SCALEFORM_MOVIE_METHOD_ADD_PARAM_BOOL = (Hash)0xC58424BA936EB458;
        private static readonly Hash SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT = (Hash)0xC3D0841A0CC546A6;
        private static readonly Hash SCALEFORM_MOVIE_METHOD_ADD_PARAM_PLAYER_NAME_STRING = (Hash)0xE83A3E3557A56640;
#endif

        #endregion

        #region Public Properties

        /// <summary>
        /// The title of the loading screen.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The subtitle of the loading screen.
        /// </summary>
        public string Subtitle { get; set; }
        /// <summary>
        /// The description of the loading screen.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The Texture Dictionary (TXD) where the texture is loaded.
        /// </summary>
        public string Dictionary { get; private set; }
        /// <summary>
        /// The texture in the dictionary.
        /// </summary>
        public string Texture { get; private set; }

        #endregion

        #region Constructors

        public LoadingScreen(string title, string subtitle, string description) : this(title, subtitle, description, "", "")
        {
        }

        public LoadingScreen(string title, string subtitle, string description, string dictionary, string texture)
        {
            // Create the scaleform and align it to the left
#if FIVEM
            scaleform = API.RequestScaleformMovie("GTAV_ONLINE");

            API.CallScaleformMovieMethod(scaleform, "HIDE_ONLINE_LOGO");

            API.BeginScaleformMovieMethod(scaleform, "SETUP_BIGFEED");
            API.ScaleformMovieMethodAddParamBool(false);
            API.EndScaleformMovieMethod();
#else
            scaleform = Function.Call<int>(Hash.REQUEST_SCALEFORM_MOVIE, "GTAV_ONLINE");

            Function.Call(CALL_SCALEFORM_MOVIE_METHOD, scaleform, "HIDE_ONLINE_LOGO");

            Function.Call(BEGIN_SCALEFORM_MOVIE_METHOD, scaleform, "SETUP_BIGFEED");
            Function.Call(SCALEFORM_MOVIE_METHOD_ADD_PARAM_BOOL, false);
            Function.Call(END_SCALEFORM_MOVIE_METHOD);
#endif
            // Save the values
            Title = title;
            Subtitle = subtitle;
            Description = description;
            Dictionary = dictionary;
            Texture = texture;
            // And send them back to the scaleform
            Update();
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Changes the texture shown on the loading screen.
        /// </summary>
        /// <param name="dictionary">The Texture Dictionary or TXD.</param>
        /// <param name="texture">The Texture name.</param>
        public void ChangeTexture(string dictionary, string texture)
        {
            Dictionary = dictionary;
            Texture = texture;
            Update();
        }
        /// <summary>
        /// Updates the Title, Description and Image of the loading screen.
        /// </summary>
        public void Update()
        {
#if FIVEM
            API.BeginScaleformMovieMethod(scaleform, "SET_BIGFEED_INFO");
            API.ScaleformMovieMethodAddParamPlayerNameString("footerStr");
            API.ScaleformMovieMethodAddParamPlayerNameString(Description);
            API.ScaleformMovieMethodAddParamInt(0);
            API.ScaleformMovieMethodAddParamPlayerNameString(Dictionary);
            API.ScaleformMovieMethodAddParamPlayerNameString(Texture);
            API.ScaleformMovieMethodAddParamPlayerNameString(Subtitle);
            API.ScaleformMovieMethodAddParamPlayerNameString("");  // urlDeprecated
            API.ScaleformMovieMethodAddParamPlayerNameString(Title);
            API.EndScaleformMovieMethod();
#else
            Function.Call(BEGIN_SCALEFORM_MOVIE_METHOD, scaleform, "SET_BIGFEED_INFO");
            Function.Call(SCALEFORM_MOVIE_METHOD_ADD_PARAM_PLAYER_NAME_STRING, "footerStr");
            Function.Call(SCALEFORM_MOVIE_METHOD_ADD_PARAM_PLAYER_NAME_STRING, Description);
            Function.Call(SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, 0);
            Function.Call(SCALEFORM_MOVIE_METHOD_ADD_PARAM_PLAYER_NAME_STRING, Dictionary);
            Function.Call(SCALEFORM_MOVIE_METHOD_ADD_PARAM_PLAYER_NAME_STRING, Texture);
            Function.Call(SCALEFORM_MOVIE_METHOD_ADD_PARAM_PLAYER_NAME_STRING, Subtitle);
            Function.Call(SCALEFORM_MOVIE_METHOD_ADD_PARAM_PLAYER_NAME_STRING, ""); // urlDeprecated
            Function.Call(SCALEFORM_MOVIE_METHOD_ADD_PARAM_PLAYER_NAME_STRING, Title);
            Function.Call(END_SCALEFORM_MOVIE_METHOD);
#endif
        }

        #endregion
    }
}
