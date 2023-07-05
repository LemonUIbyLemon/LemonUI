namespace LemonUI.Scaleform
{
    /// <summary>
    /// The Countdown scaleform in the GTA Online races.
    /// </summary>
    public class Countdown : BaseScaleform
    {
        #region Properties

        /// <summary>
        /// The duration of the countdown.
        /// </summary>
        public int Duration { get; set; } = 3;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new countdown scaleform.
        /// </summary>
        public Countdown() : base("COUNTDOWN")
        {
        }

        #endregion

        #region Functions

        /// <inheritdoc/>
        public override void Update()
        {
        }

        #endregion
    }
}
