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
		Text(const std::string& text);
		Text();

		void setText(const std::string& text) { this->m_text = std::make_unique<std::string>(text); }

		void render(const vec2& pos) const;

	private:
		std::unique_ptr<std::string> m_text = nullptr;

		int m_font = 0;
		vec4 m_color{ 1.0f };
		float m_scale = 1.0f;
		TextAlignment m_align = TA_Left;

		bool m_dropShadow = false;
		bool m_outline = false;

		bool m_wrapping = false;
		vec2 m_wrapSize{  };
	};
}