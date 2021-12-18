#include <natives.hpp>
#include "Screen.hpp"

namespace LemonUI
{
	float Screen::GetAspectRatio()
	{
		return GRAPHICS::GET_ASPECT_RATIO_(false);
	}
}
