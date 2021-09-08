#pragma once

namespace LemonUI
{
	/// <summary>
	/// Represents an item that can be drawn.
	/// </summary>
	class IDrawable
	{
	public:
		/// <summary>
		/// Draws the item on the screen.
		/// </summary>
		virtual void Draw() = 0;
	};
}
