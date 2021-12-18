#pragma once

namespace LemonUI
{
	/// <summary>
	/// Represents the internal alignment of screen elements.
	/// </summary>
	enum GFXAlignment
	{
        /// <summary>
        /// Vertical Alignment to the Bottom.
        /// </summary>
        Bottom = 66,
        /// <summary>
        /// Vertical Alignment to the Top.
        /// </summary>
        Top = 84,
        /// <summary>
        /// Centered Vertically or Horizontally.
        /// </summary>
        Center = 67,
        /// <summary>
        /// Horizontal Alignment to the Left.
        /// </summary>
        Left = 76,
        /// <summary>
        /// Horizontal Alignment to the Right.
        /// </summary>
        Right = 82,
	};

    /// <summary>
    /// Contains a set of tools to work with the screen information.
    /// </summary>
    class Screen
    {
    public:
        float GetAspectRatio();
    };
}
