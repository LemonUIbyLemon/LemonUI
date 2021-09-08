#pragma once

namespace LemonUI
{
	/// <summary>
	/// Interface for classes that have values that need to be recalculated on resolution changes.
	/// </summary>
	class IRecalculable
	{
	public:
		/// <summary>
		/// Recalculates the values.
		/// </summary>
		virtual void Recalculate() = 0;
	};
}
