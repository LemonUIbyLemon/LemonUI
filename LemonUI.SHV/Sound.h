#pragma once
#include <string>
#include <memory>

namespace LemonUI
{
	class Sound
	{
	public:
		Sound(const std::string& sound, const std::string& set);

		std::string getSoundName() { return *this->_soundName.get(); }
		void setSoundName(const std::string& sound) { this->_soundName = std::make_unique<std::string>(sound); }

		std::string getSetName() { return *this->_setName.get(); }
		void setSetName(const std::string& set) { this->_setName = std::make_unique<std::string>(set); }

		void playFrontend() const;

	private:
		std::unique_ptr<std::string> _soundName = nullptr;
		std::unique_ptr<std::string> _setName = nullptr;
	};
}