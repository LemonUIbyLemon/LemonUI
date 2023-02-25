namespace LemonUI.Scaleform
{
    /// <summary>
    /// Event information after an the BruteForce hack has finished.
    /// </summary>
    public class BruteForceFinishedEventArgs
    {
        /// <summary>
        /// The final status of the Hack.
        /// </summary>
        public BruteForceStatus Status { get; }

        internal BruteForceFinishedEventArgs(BruteForceStatus status)
        {
            Status = status;
        }
    }
}
