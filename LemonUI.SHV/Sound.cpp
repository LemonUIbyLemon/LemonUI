#include <natives.hpp>
#include <string>
#include "Sound.hpp"

namespace LemonUI
{
	Sound::Sound(std::string set, std::string file)
	{
		this->set = set;
		this->file = file;
	}

	std::string Sound::GetSet()
	{
		return this->set;
	}

	void Sound::SetSet(std::string set)
	{
		this->set = set;
	}

	std::string Sound::GetFile()
	{
		return this->file;
	}

	void Sound::SetFile(std::string file)
	{
		this->file = file;
	}

	void Sound::PlayFrontend()
	{
		AUDIO::PLAY_SOUND_FRONTEND(-1, file.c_str(), set.c_str(), false);
		int id = AUDIO::GET_SOUND_ID();
		AUDIO::RELEASE_SOUND_ID(id);
	}
}
