namespace LemonUI
{
    /// <summary>
    /// Represents the method that reports a Resolution change in the Game Settings.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="ResolutionChangedEventArgs"/> containing the Previous and Current resolution.</param>
    public delegate void ResolutionChangedEventHandler(object sender, ResolutionChangedEventArgs e);
}
