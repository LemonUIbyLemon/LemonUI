#pragma once
#include "Helper.h"

#include <string>
#include <memory>

namespace LemonUI
{
	enum TextAlignment
	{
		TA_Left,
		TA_Center,
		TA_Right
	};

	class Text
	{
	public:
		Text();
		Text(const std::string& text);

		void render(const vec2& pos);

	private:
		std::unique_ptr<std::string> _text = nullptr;

		int _font = 0;
		vec4 _color{ 1.0f };
		float _scale = 1.0f;
		TextAlignment _align = TA_Left;

		bool _dropShadow = false;
		bool _outline = false;

		bool _wrapping = false;
		vec2 _wrapSize{  };
	};
}