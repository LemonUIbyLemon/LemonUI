using System;

namespace LemonUI.Scaleform
{
    /// <summary>
    /// A warning pop-up.
    /// </summary>
    public class PopUp : BaseScaleform
    {
        private string title;
        private string subtitle;
        private string prompt;
        private string error;

        #region Properties

        /// <summary>
        /// The title of the Pop-up.
        /// </summary>
        public string Title
        {
            get => title;
            set => title = value ?? throw new ArgumentNullException(nameof(value));
        }
        /// <summary>
        /// The subtitle of the Pop-up.
        /// </summary>
        public string Subtitle
        {
            get => subtitle;
            set => subtitle = value ?? throw new ArgumentNullException(nameof(value));
        }
        /// <summary>
        /// The prompt of the Pop-up.
        /// </summary>
        public string Prompt
        {
            get => prompt;
            set => prompt = value ?? throw new ArgumentNullException(nameof(value));
        }
        /// <summary>
        /// If the black background should be shown.
        /// </summary>
        public bool ShowBackground { get; set; } = true;
        /// <summary>
        /// The error message to show.
        /// </summary>
        public string Error
        {
            get => error;
            set => error = value ?? throw new ArgumentNullException(nameof(value));
        }

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
