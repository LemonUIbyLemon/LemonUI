using System;

namespace LemonUI
{
    /// <summary>
    /// Represents a container that can hold other UI Elements.
    /// </summary>
    public interface IContainer<T> : IRecalculable, IProcessable
    {
        /// <summary>
        /// Adds the specified item into the Container.
        /// </summary>
        /// <param name="item">The item to add.</param>
        void Add(T item);
        /// <summary>
        /// Removes the item from the container.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        void Remove(T item);
        /// <summary>
        /// Removes all of the items that match the function.
        /// </summary>
        /// <param name="func">The function to check items.</param>
        void Remove(Func<T, bool> func);
        /// <summary>
        /// Clears all of the items in the container.
        /// </summary>
        void Clear();
        /// <summary>
        /// Checks if the item is part of the container.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns><see langword="true"/> if the item is in this container, <see langword="false"/> otherwise.</returns>
        bool Contains(T item);
    }
}
