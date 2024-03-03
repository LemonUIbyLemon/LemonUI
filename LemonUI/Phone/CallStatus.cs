namespace LemonUI.Phone
{
    /// <summary>
    /// The status of the current call.
    /// </summary>
    internal enum CallStatus
    {
        /// <summary>
        /// The contact is not being called.
        /// </summary>
        Inactive = -1,
        /// <summary>
        /// The contact has been triggered and is currently idling.
        /// </summary>
        Idle = 0,
        /// <summary>
        /// The contact is currently being called.
        /// </summary>
        Calling = 1,
        /// <summary>
        /// The contact has been called and has answered as Busy or Connected.
        /// </summary>
        Called = 2,
        /// <summary>
        /// The contact is busy.
        /// </summary>
        Busy = 3,
        /// <summary>
        /// The contact has been connected.
        /// </summary>
        Connected = 4
    }
}
