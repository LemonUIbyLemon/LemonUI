#pragma once
#include "Vectors.h"

#include <string>

namespace LemonUI
{
	extern void showNotify(const std::string& message);
	extern Vec2 getScreenResolution();
	extern float getAspectRatio();
	extern void toRelative(const float& absoluteX, const float& absoluteY, float* relativeX, float* relativeY);
}