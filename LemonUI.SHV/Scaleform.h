#pragma once
#include "Vectors.h"

#include <string>

namespace LemonUI
{
	class Scaleform
	{
	public:
		Scaleform(const char* name);
		Scaleform(const std::string& name);
		virtual ~Scaleform();

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

		void render2DFullScreen() const;
		void render2D(const Vec2& pos, const Vec2& res) const;

	private:
		int m_handle = 0;
	};
}