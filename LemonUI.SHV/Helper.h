#pragma once
#include <string>

namespace LemonUI
{
	extern void showNotify(const std::string& message);

	struct vec2
	{
		float x, y;

		vec2(float value)
		{
			this->x = value;
			this->y = value;
		}
		vec2()
		{
			this->x = 1.0f;
			this->y = 1.0f;
		}
	};

	struct vec4
	{
		float r, g, b, a;

		vec4(float value)
		{
			this->r = value;
			this->g = value;
			this->b = value;
			this->a = value;
		}
		vec4()
		{
			this->r = 1.0f;
			this->g = 1.0f;
			this->b = 1.0f;
			this->a = 1.0f;
		}
	};
}