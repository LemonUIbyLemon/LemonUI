#pragma once
#include "Vectors.h"
#include "Text.h"
#include "Texture.h"
#include "MenuItem.h"

#include <string>
#include <vector>

namespace LemonUI
{
	class MenuItem;

	class Menu
	{
	public:
		Menu();
		virtual ~Menu();

		MenuItem* addItem(const char* text);
		MenuItem* addItem(const std::string& text);
		MenuItem* addItem();
		void deleteItem(int index);
		void deleteItem(MenuItem* item);

		void goUp();
		void goDown();

		MenuItem* selectedItem() const;
		int selectedIndex() const { return this->m_itemSelected; };

		float contentsHeight();

		float getWidth() const { return this->m_width; }

		void setOrigin(const Vec2& origin) { this->m_origin = origin; }

		void render();

	private:
		Vec2 m_origin{ 128, 256 };
		float m_width = 431.0f;

		bool m_hasBanner = true;

		Text m_title{ "LemonUI"};
		Text m_subTitle{ "Test menu" };

		Texture m_banner{};
		Texture m_background{};

		std::vector<MenuItem*> m_items{};
		int m_itemSelected = 0;
	};
}