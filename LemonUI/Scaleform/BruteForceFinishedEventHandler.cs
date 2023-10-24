namespace LemonUI.Scaleform
{

    /// <summary>
    /// Represents the method that is called when the end user finishes the BruteForce hack.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An <see cref="BruteForceFinishedEventArgs"/> with the hack status.</param>
    public delegate void BruteForceFinishedEventHandler(object sender, BruteForceFinishedEventArgs e);
}
