// Taken from the .NET Runtime Repository
// https://github.com/dotnet/runtime
// Copyright (c) .NET Foundation and Contributors
// Under the MIT License
#if FIVEM
using System;

namespace LemonUI // Previously System.ComponentModel
{
    /// <summary>
    /// Represents the method that will handle the event raised when canceling an event.
    /// </summary>
    public delegate void CancelEventHandler(object sender, CancelEventArgs e);

    /// <summary>
    /// EventArgs used to describe a cancel event.
    /// </summary>
    public class CancelEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets a value indicating whether we should cancel the operation or not
        /// </summary>
        public bool Cancel { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public CancelEventArgs()
        {
        }

        /// <summary>
        /// Helper constructor
        /// </summary>
        /// <param name="cancel"></param>
        public CancelEventArgs(bool cancel) => Cancel = cancel;
    }
}
#endif
