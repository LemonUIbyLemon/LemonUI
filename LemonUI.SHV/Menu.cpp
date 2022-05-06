#include "pch.h"
#include "Menu.h"
#include "GameControl.h"

#include <shv/natives.h>

namespace LemonUI
{
	Menu::Menu()
	{
		this->m_title.setFont(TextFont::HouseScript);
		this->m_title.setAlign(TextAlignment::TA_Center);

		this->m_subTitle.setFont(TextFont::ChaletLondon);
		this->m_subTitle.setScale(0.35f);
		this->m_subTitle.setColor({ 93.0f, 182.0f, 229.0f, 255.0f });

		this->m_banner.set("commonmenu", "interaction_bgd");
		this->m_background.set("commonmenu", "gradient_bgd");
	}

	Menu::~Menu()
	{
		for (MenuItem* i : this->m_items)
		{
			delete i;
		}
	}

	MenuItem* Menu::addItem()
	{
		MenuItem* item = new MenuItem(this);
		this->m_items.push_back(item);
		return item;
	}
	MenuItem* Menu::addItem(const char* name)
	{
		MenuItem* item = this->addItem();
		item->changeText(name);
		return item;
	}
	MenuItem* Menu::addItem(const std::string& name)
	{
		return this->addItem(name.c_str());
	}
	
	void Menu::deleteItem(int index)
	{
		if (index < 0 || index >= this->m_items.size())
		{
			return;
		}

		delete this->m_items[index];
		this->m_items.erase(this->m_items.begin() + index);
	}
	void Menu::deleteItem(MenuItem* item)
	{
		std::vector<MenuItem*>::iterator it = std::find(this->m_items.begin(), this->m_items.end(), item);
		if (it != this->m_items.end())
		{
			this->m_items.erase(it);
		}
		delete item;
	}

	void Menu::goUp()
	{
		this->m_itemSelected--;
		if (this->m_itemSelected < 0)
		{
			this->m_itemSelected = (int)this->m_items.size() - 1;
		}

		AUDIO::PLAY_SOUND_FRONTEND(-1, const_cast<char*>("NAV_UP_DOWN"), const_cast<char*>("HUD_FRONTEND_DEFAULT_SOUNDSET"), 0);
		AUDIO::RELEASE_SOUND_ID(AUDIO::GET_SOUND_ID());
	}
	void Menu::goDown()
	{
		this->m_itemSelected++;
		if (this->m_itemSelected >= this->m_items.size())
		{
			this->m_itemSelected = 0;
		}

		AUDIO::PLAY_SOUND_FRONTEND(-1, const_cast<char*>("NAV_UP_DOWN"), const_cast<char*>("HUD_FRONTEND_DEFAULT_SOUNDSET"), 0);
		AUDIO::RELEASE_SOUND_ID(AUDIO::GET_SOUND_ID());
	}

	MenuItem* Menu::selectedItem() const
	{
		return (this->m_itemSelected < 0 || this->m_itemSelected >= this->m_items.size()) ? nullptr : this->m_items[this->m_itemSelected];
	}

	float Menu::contentsHeight()
	{
		float ret = 0.0f;
		for (MenuItem* item : this->m_items)
		{
			ret += item->getHeight();
		}

		return ret;
	}

	void Menu::render()
	{
		if (this->m_items.size() > 0)
		{
			if (CONTROLS::IS_CONTROL_JUST_PRESSED(0, GC_PhoneUp))
			{
				this->goUp();
			}
			else if (CONTROLS::IS_CONTROL_JUST_PRESSED(0, GC_PhoneDown))
			{
				this->goDown();
			}
		}

		Vec2 cursor = this->m_origin;
		if (this->m_hasBanner)
		{
			Vec2 titleRender{ cursor.x + (this->m_width / 2.0f), cursor.y + 20.0f };
			this->m_title.render(titleRender);
			this->m_banner.render(cursor, { this->m_width, 107.0f });
			cursor = { cursor.x, cursor.y + 107.0f };
		}

		Vec2 subTitleRender{ cursor.x + 10.0f, cursor.y + 2.0f };
		this->m_subTitle.render(subTitleRender);
		cursor = { cursor.x, cursor.y + 37.0f };

		this->m_background.render(cursor, { this->m_width, this->contentsHeight() });

		for (int i = 0; i < this->m_items.size(); i++)
		{
			MenuItem* item = this->m_items[i];
			item->render(cursor);
			cursor = { cursor.x, cursor.y + item->getHeight()};
		}
	}
}