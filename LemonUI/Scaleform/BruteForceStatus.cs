namespace LemonUI.Scaleform
{
    /// <summary>
    /// The status of the BruteForce Hack after finishing.
    /// </summary>
    public enum BruteForceStatus
    {
        /// <summary>
        /// The user completed the hack successfully.
        /// </summary>
        Completed = 0,
        /// <summary>
        /// The user ran out of time.
        /// </summary>
        OutOfTime = 1,
        /// <summary>
        /// The player ran out of lives.
        /// </summary>
        OutOfLives = 2,
    }
}
