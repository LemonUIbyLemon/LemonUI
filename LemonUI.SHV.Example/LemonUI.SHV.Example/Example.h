#pragma once
#include "pch.h"

#include <LemonUI/Helper.h>
#include <LemonUI/Vectors.h>
#include <LemonUI/Sound.h>
#include <LemonUI/Text.h>
#include <LemonUI/Scaleform.h>
#include <LemonUI/Menu.h>

#include <shv/natives.h>

class ExampleClass
{
private:
    LemonUI::Text* m_text = nullptr;
    LemonUI::Scaleform* m_playerList = new LemonUI::Scaleform{ "mp_mm_card_freemode" };
    LemonUI::Menu m_menu;

    bool m_playerListFocus = false;
    bool m_soundPlayed = false;

public:
    void initMenu()
    {
        LemonUI::Vec2 currentRes = LemonUI::getScreenResolution();
        this->m_menu.setOrigin({ currentRes.x / 2, currentRes.y / 2 });
        m_menu.addItem("The first item");
        m_menu.addItem("The second item");
        m_menu.addItem("The last item");
        m_menu.addItem("Go back");
    }
    void renderMenu()
    {
        m_menu.render();
    }

    void renderText()
    {
        if (this->m_text == nullptr)
        {
            return;
        }
        LemonUI::Vec2 currentRes = LemonUI::getScreenResolution();
        this->m_text->render({ currentRes.x / 2, currentRes.y - 60 });
    }
    void deleteCreateText()
    {
        if (this->m_text == nullptr)
        {
            this->m_text = new LemonUI::Text{ "Created with LemonUI.SHV by EntenKoeniq" };
            this->m_text->setScale(0.35f);
            this->m_text->setDropShadow(true);
        }
        else
        {
            delete this->m_text;
            this->m_text = nullptr;
        }
    }

    void renderPlayersList()
    {
        if (this->m_playerList == nullptr || !this->m_playerListFocus)
        {
            return;
        }

        this->m_playerList->startFunction("SET_DATA_SLOT_EMPTY");
        this->m_playerList->pushParam(0);
        this->m_playerList->finishFunction();

        this->m_playerList->startFunction("SET_DATA_SLOT");
        this->m_playerList->pushParam(0);
        this->m_playerList->pushParam("16ms");
        this->m_playerList->pushParam("EntenKoeniq");
        this->m_playerList->pushParam(116);
        this->m_playerList->pushParam(0);
        this->m_playerList->pushParam(0);
        this->m_playerList->pushParam("");
        this->m_playerList->pushParam("");
        this->m_playerList->pushParam(2);
        this->m_playerList->pushParam("");
        this->m_playerList->pushParam("");
        this->m_playerList->pushParam(" ");
        this->m_playerList->finishFunction();

        this->m_playerList->startFunction("SET_TITLE");
        this->m_playerList->pushParam("Player list");
        this->m_playerList->pushParam("1 players");
        this->m_playerList->finishFunction();

        this->m_playerList->callFunction("DISPLAY_VIEW");

        this->m_playerList->render2D({ 0.122f, 0.3f }, { 0.28f, 0.6f });
    }
    
    void focusPlayers()
    {
        if (this->m_playerListFocus)
        {
            LemonUI::showNotify("~r~PlayerList");
        }
        else
        {
            LemonUI::showNotify("~g~PlayerList");
        }

        this->m_playerListFocus = !this->m_playerListFocus;
    }
};

extern ExampleClass* _pGame = nullptr;