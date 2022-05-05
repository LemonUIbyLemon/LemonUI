#pragma once
#include "pch.h"

#include <LemonUI/Helper.h>
#include <LemonUI/Vectors.h>
#include <LemonUI/Sound.h>
#include <LemonUI/Text.h>
#include <LemonUI/Scaleform.h>

#include <shv/natives.h>

class ExampleClass
{
private:
    //LemonUI::Sound m_sound{ "Shard_Disappear", "GTAO_FM_Events_Soundset" };
    LemonUI::Text* m_text = nullptr;
    LemonUI::Scaleform* m_playerList = new LemonUI::Scaleform{ "mp_mm_card_freemode" };

    bool m_playerListFocus = false;
    bool m_soundPlayed = false;

public:
    void renderText()
    {
        if (this->m_text == nullptr)
        {
            return;
        }
        this->m_text->render(LemonUI::Vec2(0));
    }
    void deleteText()
    {
        if (this->m_text == nullptr)
        {
            this->m_text = new LemonUI::Text{ "LemonUI.SHV V1.0.0" };
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
        if (this->m_playerList == nullptr)
        {
            return;
        }

        if (this->m_playerListFocus)
        {
            if (this->m_playerList->isValid())
            {
                if (!this->m_playerList->isLoaded())
                {
                    LemonUI::showNotify("NOT LOADED!");
                }
            }
            else
            {
                LemonUI::showNotify("NOT VALID!");
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

            LemonUI::Vec2 pos{ 0.122f, 0.3f };
            LemonUI::Vec2 res{ 0.28f, 0.6f };
            this->m_playerList->render(pos, res);
        }
    }
    void deletePlayerList()
    {
        if (this->m_playerList == nullptr)
        {
            this->m_playerList = new LemonUI::Scaleform{ "mp_mm_card_freemode" };
        }
        else
        {
            delete this->m_playerList;
            this->m_playerList = nullptr;
        }
    }
    void focusPlayers()
    {
        if (m_playerList == nullptr)
        {
            return;
        }
        this->m_playerListFocus = !this->m_playerListFocus;
        
        //if (this->m_sound.requestRef(false))
        //{
        //    this->m_sound.playFrontend(false);
        //    this->m_soundPlayed = true;
        //}
    }
};

extern ExampleClass* _pGame = nullptr;