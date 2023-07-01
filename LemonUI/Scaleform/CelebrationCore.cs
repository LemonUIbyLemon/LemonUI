namespace LemonUI.Scaleform
{
    /// <summary>
    /// The base of all MP_CELEBRATION* scaleforms.
    /// </summary>
    public abstract class CelebrationCore : BaseScaleform
    {
        #region Constructors

        /// <summary>
        /// Initializes a new class with the core behavior of the Celebration scaleform.
        /// </summary>
        /// <param name="sc">The scaleform to use.</param>
        public CelebrationCore(string sc) : base(sc)
        {
        }

        #endregion

        #region Functions

        /// <summary>
        /// Updates the celebration scaleform.
        /// </summary>
        public override void Update()
        {
        }

        #endregion
    }
}
