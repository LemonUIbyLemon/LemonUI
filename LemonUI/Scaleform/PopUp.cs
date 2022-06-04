namespace LemonUI.Scaleform
{
    /// <summary>
    /// A warning pop-up.
    /// </summary>
    public class PopUp : BaseScaleform
    {
        #region Properties

        /// <summary>
        /// The title of the Pop-up.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The subtitle of the Pop-up.
        /// </summary>
        public string Subtitle { get; set; }
        /// <summary>
        /// The prompt of the Pop-up.
        /// </summary>
        public string Prompt { get; set; }
        /// <summary>
        /// If the black background should be shown.
        /// </summary>
        public bool ShowBackground { get; set; } = true;
        /// <summary>
        /// The error message to show.
        /// </summary>
        public string Error { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new Pop-up instance.
        /// </summary>
        public PopUp() : base("POPUP_WARNING")
        {
        }

        #endregion

        #region Functions

        /// <summary>
        /// Updates the texts of the Pop-up.
        /// </summary>
        public override void Update()
        {
            // first parameter "msecs" is unused
            CallFunction("SHOW_POPUP_WARNING", 0, Title, Subtitle, Prompt, ShowBackground, 0, Error);
        }

        #endregion
    }
}
