using System;

namespace LemonUI.Scaleform
{
    /// <summary>
    /// Scaleforms are 2D Adobe Flash-like objects.
    /// </summary>
    public interface IScaleform : IDrawable, IProcessable, IDisposable
    {
        /// <summary>
        /// If the Scaleform object is visible or not.
        /// </summary>
        bool Visible { get; set; }
        /// <summary>
        /// Draws the Scaleform in full screen.
        /// </summary>
        void DrawFullScreen();
    }
}
