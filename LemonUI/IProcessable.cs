namespace LemonUI
{
    /// <summary>
    /// Interface for items that can be processed in an Object Pool.
    /// </summary>
    public interface IProcessable
    {
        /// <summary>
        /// If this processable item is visible on the screen.
        /// </summary>
        bool Visible { get; set; }
        /// <summary>
        /// Processes the object.
        /// </summary>
        void Process();
    }
}
