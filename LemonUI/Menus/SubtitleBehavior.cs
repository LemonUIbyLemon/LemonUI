namespace LemonUI.Menus
{
    /// <summary>
    /// The behavior of the <see cref="NativeMenu"/>'s subtitle.
    /// </summary>
    public enum SubtitleBehavior
    {
        /// <summary>
        /// The subtitle will always be shown.
        /// </summary>
        AlwaysShow = 0,
        /// <summary>
        /// The subtitle will always be shown, except when is empty.
        /// </summary>
        ShowIfRequired = 1,
        /// <summary>
        /// The subtitle will never be shown.
        /// </summary>
        AlwaysHide = 2
    }
}
