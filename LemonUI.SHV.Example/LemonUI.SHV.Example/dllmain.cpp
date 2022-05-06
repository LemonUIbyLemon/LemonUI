#include "pch.h"

#include "Example.h"

#include <LemonUI/Helper.h>

#include <shv/main.h>
#include <shv/natives.h>

static void scriptKeyboardHandler(DWORD key, WORD repeats, BYTE scanCode, BOOL isExtended, BOOL isWithAlt, BOOL wasDownBefore, BOOL isUpNow)
{
    if (wasDownBefore == FALSE && isUpNow == FALSE)
    {
        if (key == VK_F3)
        {
            _pGame->focusPlayers();
        }
        else if (key == VK_F4)
        {
            _pGame->deleteCreateText();
        }
    }
}

static void scriptMainFunc()
{
    while (DLC2::GET_IS_LOADING_SCREEN_ACTIVE())
    {
        WAIT(0);
    }
    srand(GetTickCount());

    _pGame = new ExampleClass();

    LemonUI::showNotify("F3 = Show/Hide players | F4 = Delete/Create text");

    while (true)
    {        
        _pGame->renderText();
        _pGame->renderPlayersList();

        WAIT(0);
    }
}

static void scriptInitialize(HMODULE hModule)
{
    scriptRegister(hModule, scriptMainFunc);
    keyboardHandlerRegister(scriptKeyboardHandler);
}

static void scriptUninitialize(HMODULE hModule)
{
    keyboardHandlerUnregister(scriptKeyboardHandler);
    scriptUnregister(hModule);

    if (_pGame != nullptr)
    {
        delete _pGame;
    }
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved)
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
        scriptInitialize(hModule);
        break;
    case DLL_PROCESS_DETACH:
        scriptUninitialize(hModule);
        break;
    }
    return TRUE;
}

