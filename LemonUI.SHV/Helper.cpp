#include "pch.h"
#include "Helper.h"

#include <shv/natives.h>

namespace LemonUI
{
	void showNotify(const std::string& message)
	{
		UI::_SET_NOTIFICATION_TEXT_ENTRY(const_cast<char*>("STRING"));
		UI::_ADD_TEXT_COMPONENT_STRING(const_cast<char*>(message.c_str()));
		UI::_DRAW_NOTIFICATION(false, false);
	}

	Vec2 getScreenResolution()
	{
		int width, height;
		GRAPHICS::_GET_SCREEN_ACTIVE_RESOLUTION(&width, &height);
		return { static_cast<float>(width), static_cast<float>(height) };
	}

	float getAspectRatio()
	{
		return GRAPHICS::_GET_SCREEN_ASPECT_RATIO(0);
	}

	void toRelative(const float& absoluteX, const float& absoluteY, float* relativeX, float* relativeY)
	{
		Vec2 currentRes = getScreenResolution();
		float width = currentRes.y * getAspectRatio();
		*relativeX = absoluteX / width;
		*relativeY = absoluteY / currentRes.y;
	}

	Vec2 getRectCenter(const Vec2& pos, const Vec2& size)
	{
		return { pos.x + size.x / 2.0f, pos.y + size.y / 2.0f };
	}

	Vec2 getScreenScale(const Vec2& vec)
	{
		Vec2 currentRes = getScreenResolution();
		return { vec.x / currentRes.x, vec.y / currentRes.y };
	}
}