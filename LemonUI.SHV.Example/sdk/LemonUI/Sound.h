#pragma once
#include <string>

namespace LemonUI
{
	class Sound
	{
	public:
		Sound(const char* sound, const char* set);
		Sound(const std::string& sound, const std::string& set);
		virtual ~Sound();

		char*& getSoundName() { return *&this->m_name; }
		void setSoundName(const char* name) { this->m_name = const_cast<char*>(name); }
		void setSoundName(const std::string& name) { this->m_name = const_cast<char*>(name.c_str()); }

		char*& getRefName() { return *&this->m_ref; }
		void setRefName(const char* name) { this->m_ref = const_cast<char*>(name); }
		void setRefName(const std::string& name) { this->m_ref = const_cast<char*>(name.c_str()); }

		bool requestRef(bool p1);

		void play(bool p5);
		void playFrontend(bool p3);

		bool finished() const;

		void release();
		void stop() const;

	private:
		char* m_name = nullptr;
		char* m_ref = nullptr;

		// Sound ID
		int m_id = 0;

		bool m_refLoaded = false;
	};
}