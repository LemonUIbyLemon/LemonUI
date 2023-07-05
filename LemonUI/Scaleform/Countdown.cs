using System;

namespace LemonUI.Scaleform
{
    /// <summary>
    /// The Countdown scaleform in the GTA Online races.
    /// </summary>
    public class Countdown : BaseScaleform
    {
        #region Fields

        private int duration = 3;

        #endregion

        #region Properties

        /// <summary>
        /// The duration of the countdown.
        /// </summary>
        public int Duration
        {
            get => duration;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The duration can't be equal or under zero.");
                }

                duration = value;
            }
        }

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
