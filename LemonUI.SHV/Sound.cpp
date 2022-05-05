#include "pch.h"
#include "Sound.h"

#include <shv/natives.h>

/*
* I (EntenKoeniq) tried several solutions to get sounds working for me but nothing works for me.
* 
* Lemon said the sounds work for him, maybe someone else can check
* because i don't know what's wrong and i'll take the time to add other things.
*/
namespace LemonUI
{
	Sound::Sound(const char* name, const char* ref) : m_name{ const_cast<char*>(name) }, m_ref{ const_cast<char*>(ref) }
	{}
	Sound::Sound(const std::string& name, const std::string& ref) : m_name{ const_cast<char*>(name.c_str()) }, m_ref{ const_cast<char*>(ref.c_str()) }
	{}
	Sound::~Sound()
	{
		// To unload multiplayer sounds
		if (AUDIO::_0x5B50ABB1FE3746F4() == FALSE) // HAS_MULTIPLAYER_AUDIO_DATA_UNLOADED
		{
			AUDIO::SET_AUDIO_FLAG(const_cast<char*>("LoadMPData"), FALSE);
		}

		this->stop();
		this->release();
	}

	bool Sound::requestRef(bool p1)
	{
		// Don't request the same audio multiple times
		if (this->m_refLoaded)
		{
			return true;
		}
		if (this->m_ref == nullptr)
		{
			return false;
		}

		// To load muiltiplayer sounds
		if (AUDIO::_0x544810ED9DB6BBE6() == FALSE) // HAS_MULTIPLAYER_AUDIO_DATA_LOADED
		{
			AUDIO::SET_AUDIO_FLAG(const_cast<char*>("LoadMPData"), TRUE);
		}

		this->m_refLoaded = AUDIO::REQUEST_SCRIPT_AUDIO_BANK(this->m_ref, p1);

		return this->m_refLoaded;
	}

	void Sound::play(bool p5)
	{
		if (this->m_name == nullptr || this->m_ref == nullptr)
		{
			return;
		}

		AUDIO::PLAY_SOUND(-1, this->m_name, this->m_ref, 0, 0, p5);
		this->m_id = AUDIO::GET_SOUND_ID();
	}
	void Sound::playFrontend(bool p3)
	{
		if (this->m_name == nullptr || this->m_ref == nullptr)
		{
			return;
		}
		
		AUDIO::PLAY_SOUND_FRONTEND(-1, this->m_name, this->m_ref, p3);
		this->m_id = AUDIO::GET_SOUND_ID();
	}

	bool Sound::finished() const
	{
		return this->m_id != 0 ? AUDIO::HAS_SOUND_FINISHED(this->m_id) : true;
	}

	void Sound::release()
	{
		if (this->m_id == 0)
		{
			return;
		}

		AUDIO::RELEASE_SOUND_ID(this->m_id);
		this->m_id = 0;
	}
	void Sound::stop() const
	{
		if (this->m_id == 0)
		{
			return;
		}

		AUDIO::STOP_SOUND(this->m_id);
	}
}