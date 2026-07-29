#if SHVDN3
namespace LemonUI.Phone
{
    /// <summary>
    /// Represents the method that is called when the phone call is connected.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An <see cref="CalledEventArgs"/> with the connection information.</param>
    public delegate void ConnectedEventHandler(object sender, ConnectedEventArgs e);
}
#endif
