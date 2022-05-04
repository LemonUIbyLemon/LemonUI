#pragma once
#include <string>
#include <memory>

namespace LemonUI
{
	class Sound
	{
	public:
		Sound(const std::string& sound, const std::string& set);
		virtual ~Sound();

		std::string getSoundName() { return *this->m_name.get(); }
		void setSoundName(const std::string& sound) { this->m_name = std::make_unique<std::string>(sound); }

		std::string getRefName() { return *this->m_ref.get(); }
		void setRefName(const std::string& set) { this->m_ref = std::make_unique<std::string>(set); }

		bool requestRef(bool p1);

		void play(bool p5);
		void playFrontend(bool p3);

		bool finished() const;

		void release();
		void stop() const;

	private:
		std::unique_ptr<std::string> m_name = nullptr;
		std::unique_ptr<std::string> m_ref = nullptr;

		// Sound ID
		int m_id = 0;

		bool m_refLoaded = false;
	};
}