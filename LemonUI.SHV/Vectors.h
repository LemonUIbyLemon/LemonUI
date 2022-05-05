#pragma once

namespace LemonUI
{
	/// <summary>
	/// Default value = 1.0f
	/// </summary>
	struct Vec2
	{
		float x = 1.0f, y = 1.0f;

		Vec2(float x, float y)
		{
			this->x = x;
			this->y = y;
		}
		Vec2(float value)
		{
			this->x = value;
			this->y = value;
		}
		Vec2() {}
	};

	/// <summary>
	/// Default value = 1.0f
	/// </summary>
	struct Vec4
	{
		float r = 1.0f, g = 1.0f, b = 1.0f, a = 1.0f;

		Vec4(float r, float g, float b, float a)
		{
			this->r = r;
			this->g = g;
			this->b = b;
			this->a = a;
		}
		Vec4(float value)
		{
			this->r = value;
			this->g = value;
			this->b = value;
			this->a = value;
		}
		Vec4() {}
	};
}