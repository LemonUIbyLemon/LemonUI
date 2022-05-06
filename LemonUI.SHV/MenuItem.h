#pragma once
#include "Menu.h"
#include "Text.h"
#include "Texture.h"

namespace LemonUI
{
	class Menu;

	class MenuItem
	{
	public:
		MenuItem(Menu* parent);

		void changeText(const char* text) { this->m_text.setText(text); }

		float getHeight() const { return this->m_height; }
		bool isHovering() const;

		void render(const Vec2& pos);

	private:
		Menu* m_parent = nullptr;

		float m_height = 38;
		Text m_text{};
		Texture m_texture{};
	};
}