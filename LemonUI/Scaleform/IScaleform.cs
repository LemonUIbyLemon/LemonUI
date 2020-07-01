using System;

namespace LemonUI.Scaleform
{
    /// <summary>
    /// Scaleforms are 2D Adobe Flash-like objects.
    /// </summary>
    public interface IScaleform : IDrawable, IProcessable, IDisposable
    {
        /// <summary>
        /// Draws the Scaleform in full screen.
        /// </summary>
        void DrawFullScreen();
    }
}
