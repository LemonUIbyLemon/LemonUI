#pragma once
#include "pch.h"

#include <LemonUI/Sound.h>
#include <LemonUI/Text.h>
#include <LemonUI/Helper.h>
#include <LemonUI/Scaleform.h>

#include <shv/natives.h>

class ExampleClass
{
private:
    LemonUI::Sound m_sound{ "Shard_Disappear", "GTAO_FM_Events_Soundset" };
    LemonUI::Text m_text{ "LemonUI.SHV V1.0.0" };
    LemonUI::Scaleform m_playerList{ "mp_mm_card_freemode" };

    bool m_playerListFocus = false;
    bool m_soundPlayed = false;

public:
    void renderText()
    {
        this->m_text.render(LemonUI::vec2(0));
    }

    void renderPlayersList()
    {
        if (this->m_playerListFocus)
        {
            if (this->m_playerList.isValid())
            {
                if (!this->m_playerList.isLoaded())
                {
                    LemonUI::showNotify("NOT LOADED!");
                }
            }
            else
            {
                LemonUI::showNotify("NOT VALID!");
                return;
            }

            this->m_playerList.startFunction("SET_DATA_SLOT_EMPTY");
            this->m_playerList.pushParam(0);
            this->m_playerList.finishFunction();

            this->m_playerList.startFunction("SET_DATA_SLOT");
            this->m_playerList.pushParam(0);
            this->m_playerList.pushParam("16ms");
            this->m_playerList.pushParam("EntenKoeniq");
            this->m_playerList.pushParam(116);
            this->m_playerList.pushParam(0);
            this->m_playerList.pushParam(0);
            this->m_playerList.pushParam("");
            this->m_playerList.pushParam("");
            this->m_playerList.pushParam(2);
            this->m_playerList.pushParam("");
            this->m_playerList.pushParam("");
            this->m_playerList.pushParam(" ");
            this->m_playerList.finishFunction();

            this->m_playerList.startFunction("SET_TITLE");
            this->m_playerList.pushParam("Player list");
            this->m_playerList.pushParam("1 players");
            this->m_playerList.finishFunction();

            this->m_playerList.callFunction("DISPLAY_VIEW");

            LemonUI::vec2 pos{ 0.122f, 0.3f };
            LemonUI::vec2 res{ 0.28f, 0.6f };
            this->m_playerList.render(pos, res);
        }

        //if (this->m_soundPlayed && this->m_sound.finished())
        //{
        //    this->m_sound.release();
        //    this->m_soundPlayed = false;
        //}
    }
    void focusPlayers()
    {
        this->m_playerListFocus = !this->m_playerListFocus;
        
        if (this->m_sound.requestRef(false))
        {
            this->m_sound.playFrontend(false);
            this->m_soundPlayed = true;
        }
    }
};

extern ExampleClass* _pGame = nullptr;