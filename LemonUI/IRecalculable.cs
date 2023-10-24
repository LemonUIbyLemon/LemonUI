namespace LemonUI
{
    /// <summary>
    /// Interface for classes that have values that need to be recalculated on resolution changes.
    /// </summary>
    public interface IRecalculable
    {
        #region Functions

        /// <summary>
        /// Recalculates the values.
        /// </summary>
        void Recalculate();

        #endregion
    }
}
