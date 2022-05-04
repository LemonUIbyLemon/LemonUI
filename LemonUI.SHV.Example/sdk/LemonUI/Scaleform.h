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
		virtual ~Scaleform();

		void request(const char* id);
		void request(const std::string& id);
		bool isValid() const;
		bool isLoaded() const;

		void callFunction(const char* name) const;
		void callFunction(const std::string& name) const;

		void startFunction(const char* name) const;
		void startFunction(const std::string& name) const;

		void pushParam(const char* param) const;
		void pushParam(const std::string& param) const;
		void pushParam(const int& param) const;
		void pushParam(const float& param) const;
		void pushParam(const bool& param) const;

		void finishFunction() const;

		void renderFullScreen() const;
		void render(const vec2& pos, const vec2& res) const;

	private:
		int m_handle = 0;
	};
}