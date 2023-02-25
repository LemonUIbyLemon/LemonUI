namespace LemonUI
{
    /// <summary>
    /// Represents the method that reports a Safe Zone change in the Game Settings.
    /// </summary>
    /// <param name="sender">The source of the event event.</param>
    /// <param name="e">A <see cref="ResolutionChangedEventArgs"/> containing the Previous and Current Safe Zone.</param>
    public delegate void SafeZoneChangedEventHandler(object sender, SafeZoneChangedEventArgs e);
}
