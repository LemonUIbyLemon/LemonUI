#include "pch.h"
#include "Sound.h"

#include "Helper.h"

#include <shv/natives.h>

namespace LemonUI
{
	Sound::Sound(const std::string& sound, const std::string& set)
	{
		this->_soundName = std::make_unique<std::string>(sound);
		this->_setName = std::make_unique<std::string>(set);
	}

	void Sound::playFrontend() const
	{
		showNotify("\"playFrontend()\" is not working yet!");

		/* game crashes */
		//AUDIO::PLAY_SOUND_FRONTEND(-1, const_cast<char*>((*this->_soundName.get()).c_str()), const_cast<char*>((*this->_setName.get()).c_str()), true);
		//int id = AUDIO::GET_SOUND_ID();
		//AUDIO::RELEASE_SOUND_ID(id);
	}
}