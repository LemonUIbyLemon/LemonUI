#pragma once
#include "Helper.h"

#include <string>

namespace LemonUI
{
	class Scaleform
	{
	public:
		Scaleform(const char* id);
		Scaleform(const std::string& id);
		Scaleform();
		virtual ~Scaleform();

		void request(const char* id);
		void request(const std::string& id);
		bool isValid();
		bool isLoaded();

		void startFunction(const char* name);
		void startFunction(const std::string& name);
		void callFunction();

		template<typename T>
		void pushParam(const T param);

		void finishFunction();
		void callFunction(const char* name);
		void callFunction(const std::string& name);

		void render();
		void render(const vec2& pos, const vec2& size);

	private:
		int m_handle = 0;
	};
}