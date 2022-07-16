#if SHVDN3
namespace LemonUI.Phone
{
    /// <summary>
    /// Represents the method that is called when one of the properties of the contact are changed.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An <see cref="PhoneContactChangedEventArgs"/> with the contact information.</param>
    public delegate void PhoneContactChangedEventHandler(object sender, PhoneContactChangedEventArgs e);
}
#endif
