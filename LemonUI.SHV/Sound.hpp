#pragma once
#include <string>

namespace LemonUI
{
	class Sound
	{
	private:
		std::string set;
		std::string file;

	public:
		Sound(const std::string& set, const std::string& file);

		std::string GetSet();
		void SetSet(const std::string&);
		std::string GetFile();
		void SetFile(const std::string&);

		void PlayFrontend();
	};
}
