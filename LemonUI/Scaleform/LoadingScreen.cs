namespace LemonUI.Scaleform
{
    /// <summary>
    /// Loading screen like the transition between story mode and online.
    /// </summary>
    public class LoadingScreen : BaseScaleform
    {
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

        /// <summary>
        /// Creates a new GTA Online like loading screen with no image.
        /// </summary>
        /// <param name="title">The title of the screen.</param>
        /// <param name="subtitle">The subtitle of the screen.</param>
        /// <param name="description">The description of the screen.</param>
        public LoadingScreen(string title, string subtitle, string description) : this(title, subtitle, description, "", "")
        {
        }
        /// <summary>
        /// Creates a new GTA Online like loading screen with a custom texture.
        /// </summary>
        /// <param name="title">The title of the screen.</param>
        /// <param name="subtitle">The subtitle of the screen.</param>
        /// <param name="description">The description of the screen.</param>
        /// <param name="dictionary">The dictionary where the texture is located.</param>
        /// <param name="texture">The texture to use on the right.</param>
        public LoadingScreen(string title, string subtitle, string description, string dictionary, string texture) : base("GTAV_ONLINE")
        {
            // Tell the Scaleform to use the online loading screen
            CallFunction("HIDE_ONLINE_LOGO");
            CallFunction("SETUP_BIGFEED", false);
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
        public override void Update()
        {
            CallFunction("SET_BIGFEED_INFO", "footerStr", Description, 0, Dictionary, Texture, Subtitle, "urlDeprecated", Title);
        }

        #endregion
    }
}
