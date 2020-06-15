using System;

namespace LemonUI.Items
{
    /// <summary>
    /// A menu item that can go Left and Right.
    /// </summary>
    public interface ISlidableItem : IItem
    {
        #region Public Functions

        /// <summary>
        /// Moves to the Left.
        /// </summary>
        void GoLeft();
        /// <summary>
        /// Moves to the Right.
        /// </summary>
        void GoRight();

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when the item has gone to the left.
        /// </summary>
        event EventHandler Left;
        /// <summary>
        /// Event triggered when the item has gone to the right.
        /// </summary>
        event EventHandler Right;

        #endregion
    }
}
