#include "pch.h"
#include "MenuItem.h"

namespace LemonUI
{
	MenuItem::MenuItem(Menu* parent)
	{
		this->m_parent = parent;

		this->m_text.setFont(TextFont::ChaletLondon);
		this->m_text.setScale(0.35f);

		this->m_texture.set("commonmenu", "gradient_nav");
	}

	bool MenuItem::isHovering() const
	{
		return this->m_parent != nullptr ? this->m_parent->selectedItem() == this : false;
	}

	void MenuItem::render(const Vec2& pos)
	{
		if (this->isHovering())
		{
			this->m_texture.render(pos, { this->m_parent->getWidth(), this->m_height });
			this->m_text.setColor({ 0.0f, 0.0f, 0.0f, 1.0f });
		}
		else
		{
			this->m_text.setColor({ 1.0f, 1.0f, 1.0f, 1.0f });
		}


		this->m_text.render({ pos.x + 10.0f, pos.y + 2.0f });
	}
}