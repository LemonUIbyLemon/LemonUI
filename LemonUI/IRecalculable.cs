namespace LemonUI
{
    /// <summary>
    /// Interface for classes that have values that need to be recalculated on resolution changes.
    /// </summary>
    public interface IRecalculable
    {
        /// <summary>
        /// Recalculates the values.
        /// </summary>
        void Recalculate();
    }
}
