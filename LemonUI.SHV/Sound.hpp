#pragma once
#include <string>

class Sound
{
	private:
		std::string set;
		std::string file;

	public:
		Sound(std::string set, std::string file);

		std::string GetSet();
		void SetSet(std::string);
		std::string GetFile();
		void SetFile(std::string);

		void PlayFrontend();
};
