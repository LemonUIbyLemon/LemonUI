namespace LemonUI
{
    /// <summary>
    /// Interface for items that can be processed in an Object Pool.
    /// </summary>
    public interface IProcessable
    {
        /// <summary>
        /// Processes the object.
        /// </summary>
        void Process();
    }
}
