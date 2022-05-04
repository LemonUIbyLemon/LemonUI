#include "pch.h"
#include "Text.h"

#include <shv/natives.h>

#include <memory>

namespace LemonUI
{
	Text::Text() : _color(1.0f)
	{
	}
	Text::Text(const std::string& text) : _color(1.0f)
	{
		this->_text = std::make_unique<std::string>(text);
	}

	void Text::render(const vec2& pos)
	{
		UI::SET_TEXT_FONT(this->_font);
		UI::SET_TEXT_SCALE(1.0f, this->_scale);
		UI::SET_TEXT_COLOUR((int)(this->_color.r * 255.0f), (int)(this->_color.g * 255.0f), (int)(this->_color.b * 255.0f), (int)(this->_color.a * 255.0f));
		if (this->_wrapping)
		{
			UI::SET_TEXT_WRAP(pos.x, pos.x + this->_wrapSize.x);
		}
		else
		{
			UI::SET_TEXT_WRAP(0.0, 1.0);
		}
		if (this->_align == TA_Center)
		{
			UI::SET_TEXT_CENTRE(1);
		}
		else
		{
			UI::SET_TEXT_CENTRE(0);
			UI::SET_TEXT_RIGHT_JUSTIFY(1);
		}
		if (this->_dropShadow)
		{
			UI::SET_TEXT_DROP_SHADOW();
		}
		else
		{
			UI::SET_TEXT_DROPSHADOW(0, 0, 0, 0, 0);
		}
		if (this->_outline)
		{
			UI::SET_TEXT_OUTLINE();
		}
		
		UI::SET_TEXT_EDGE(1, 0, 0, 0, 205);
		UI::_SET_TEXT_ENTRY(const_cast<char*>("STRING"));
		UI::_ADD_TEXT_COMPONENT_STRING(const_cast<char*>((*this->_text.get()).c_str()));
		UI::_DRAW_TEXT(pos.x, pos.y);
	}
}