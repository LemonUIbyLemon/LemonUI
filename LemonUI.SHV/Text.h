#pragma once
#include "Vectors.h"

#include <string>

namespace LemonUI
{
	enum TextAlignment
	{
		TA_Left,
		TA_Center,
		TA_Right
	};

	enum TextFont : uint8_t
	{
		ChaletLondon = 0,
		HouseScript = 1,
		Monospace = 2,
		CharletComprimeColonge = 4,
		Pricedown = 7
	};

	class Text
	{
	public:
		Text(const char* text);
		Text(const std::string& text);
		Text() = default;

		void setText(const char* text) { this->m_text = const_cast<char*>(text); }
		void setText(const std::string& text) { this->m_text = const_cast<char*>(text.c_str()); }
		void setAlign(const TextAlignment& align) { this->m_align = align; }
		void setScale(const float& scale) { this->m_scale = scale; }
		void setColor(const Vec4& color) { this->m_color = color; }
		void setFont(const TextFont& font) { this->m_font = (int)font; }
		void setDropShadow(const bool& value) { this->m_dropShadow = value; }
		void setOutline(const bool& value) { this->m_outline = value; }
		void setWrapping(const bool& value, const Vec2& size) { this->m_wrapping = value; this->m_wrapSize = size; }

		void render(const Vec2& pos) const;

	private:
		char* m_text = nullptr;

		int m_font = 0;
		Vec4 m_color = { 1.0f, 1.0f, 1.0f, 1.0f };
		float m_scale = 1.0f;
		TextAlignment m_align = TA_Center;

		bool m_dropShadow = false;
		bool m_outline = false;

		bool m_wrapping = false;
		Vec2 m_wrapSize{};
	};
}