#include "pch.h"
#include "Text.h"

#include <shv/natives.h>

#include <memory>

namespace LemonUI
{
	Text::Text(const std::string& text) : m_color(1.0f)
	{
		this->m_text = std::make_unique<std::string>(text);
	}
	Text::Text() : m_color(1.0f)
	{}

	void Text::render(const vec2& pos) const
	{
		if (this->m_text == nullptr)
		{
			return;
		}

		UI::SET_TEXT_FONT(this->m_font);
		UI::SET_TEXT_SCALE(1.0f, this->m_scale);
		UI::SET_TEXT_COLOUR((int)(this->m_color.r * 255.0f), (int)(this->m_color.g * 255.0f), (int)(this->m_color.b * 255.0f), (int)(this->m_color.a * 255.0f));
		if (this->m_wrapping)
		{
			UI::SET_TEXT_WRAP(pos.x, pos.x + this->m_wrapSize.x);
		}
		else
		{
			UI::SET_TEXT_WRAP(0.0, 1.0);
		}
		if (this->m_align == TA_Center)
		{
			UI::SET_TEXT_CENTRE(1);
		}
		else
		{
			UI::SET_TEXT_CENTRE(0);
			UI::SET_TEXT_RIGHT_JUSTIFY(1);
		}
		if (this->m_dropShadow)
		{
			UI::SET_TEXT_DROP_SHADOW();
		}
		else
		{
			UI::SET_TEXT_DROPSHADOW(0, 0, 0, 0, 0);
		}
		if (this->m_outline)
		{
			UI::SET_TEXT_OUTLINE();
		}
		
		UI::SET_TEXT_EDGE(1, 0, 0, 0, 205);
		UI::_SET_TEXT_ENTRY(const_cast<char*>("STRING"));
		UI::_ADD_TEXT_COMPONENT_STRING(const_cast<char*>((*this->m_text.get()).c_str()));
		UI::_DRAW_TEXT(pos.x, pos.y);
	}
}