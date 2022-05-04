#include "pch.h"
#include "Sound.h"

#include <shv/natives.h>

namespace LemonUI
{
	Sound::Sound(const std::string& sound, const std::string& ref)
	{
		this->m_name = std::make_unique<std::string>(sound);
		this->m_ref = std::make_unique<std::string>(ref);
	}
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

		// To load muiltiplayer sounds
		if (AUDIO::_0x544810ED9DB6BBE6() == FALSE) // HAS_MULTIPLAYER_AUDIO_DATA_LOADED
		{
			AUDIO::SET_AUDIO_FLAG(const_cast<char*>("LoadMPData"), TRUE);
		}

		this->m_refLoaded = AUDIO::REQUEST_SCRIPT_AUDIO_BANK(const_cast<char*>((*this->m_ref.get()).c_str()), p1);

		return this->m_refLoaded;
	}

	void Sound::play(bool p5)
	{
		if (this->m_name == nullptr || this->m_ref == nullptr)
		{
			return;
		}

		AUDIO::PLAY_SOUND(-1, const_cast<char*>((*this->m_name.get()).c_str()), const_cast<char*>((*this->m_ref.get()).c_str()), 0, 0, p5);
		this->m_id = AUDIO::GET_SOUND_ID();
	}
	void Sound::playFrontend(bool p3)
	{
		if (this->m_name == nullptr || this->m_ref == nullptr)
		{
			return;
		}
		
		AUDIO::PLAY_SOUND_FRONTEND(-1, const_cast<char*>((*this->m_name.get()).c_str()), const_cast<char*>((*this->m_ref.get()).c_str()), p3);
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