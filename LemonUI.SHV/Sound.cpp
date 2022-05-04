#include "pch.h"
#include "Sound.h"

#include <shv/natives.h>

namespace LemonUI
{
	Sound::Sound(const std::string& sound, const std::string& set)
	{
		this->_soundName = std::make_unique<std::string>(sound);
		this->_setName = std::make_unique<std::string>(set);
	}
	Sound::Sound()
	{}

	void Sound::playFrontend() const
	{
		std::string soundName = *this->_soundName.get();
		std::string setName = *this->_setName.get();
		
		AUDIO::PLAY_SOUND_FRONTEND(-1, const_cast<char*>(soundName.c_str()), const_cast<char*>(setName.c_str()), false);
		int id = AUDIO::GET_SOUND_ID();
		AUDIO::RELEASE_SOUND_ID(id);
	}
}