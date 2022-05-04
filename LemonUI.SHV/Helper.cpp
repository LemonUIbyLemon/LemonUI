#include "pch.h"
#include "Helper.h"

#include <shv/natives.h>

namespace LemonUI
{
	void showNotify(const std::string& message)
	{
		UI::_SET_NOTIFICATION_TEXT_ENTRY(const_cast<char*>("STRING"));
		UI::_ADD_TEXT_COMPONENT_STRING(const_cast<char*>(message.c_str()));
		UI::_DRAW_NOTIFICATION(false, false);
	}
}