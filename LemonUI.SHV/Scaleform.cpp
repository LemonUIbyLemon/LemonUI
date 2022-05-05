#include "pch.h"
#include "Scaleform.h"

#include <shv/natives.h>

namespace LemonUI
{
	Scaleform::Scaleform(const std::string& name)
	{
		this->m_handle = GRAPHICS::REQUEST_SCALEFORM_MOVIE(const_cast<char*>(name.c_str()));
	}
	Scaleform::Scaleform(const char* name)
	{
		this->m_handle = GRAPHICS::REQUEST_SCALEFORM_MOVIE(const_cast<char*>(name));
	}
	Scaleform::~Scaleform()
	{
		if (this->m_handle != 0 && this->isLoaded())
		{
			GRAPHICS::SET_SCALEFORM_MOVIE_AS_NO_LONGER_NEEDED(&this->m_handle);
		}
	}

	bool Scaleform::isValid() const
	{
		return this->m_handle != 0;
	}
	bool Scaleform::isLoaded() const
	{
		return GRAPHICS::HAS_SCALEFORM_MOVIE_LOADED(this->m_handle) == TRUE;
	}

	void Scaleform::callFunction(const char* name) const
	{
		GRAPHICS::CALL_SCALEFORM_MOVIE_METHOD(this->m_handle, const_cast<char*>(name));
	}
	void Scaleform::callFunction(const std::string& name) const
	{
		this->callFunction(name.c_str());
	}

	void Scaleform::startFunction(const char* name) const
	{
		GRAPHICS::_PUSH_SCALEFORM_MOVIE_FUNCTION(this->m_handle, const_cast<char*>(name));
	}
	void Scaleform::startFunction(const std::string& name) const
	{
		this->startFunction(name.c_str());
	}

	void Scaleform::pushParam(const char* param) const
	{
		GRAPHICS::_PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_STRING(const_cast<char*>(param));
	}
	void Scaleform::pushParam(const std::string& param) const
	{
		this->pushParam(param.c_str());
	}
	void Scaleform::pushParam(const int& param) const
	{
		GRAPHICS::_PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT(param);
	}
	void Scaleform::pushParam(const float& param) const
	{
		GRAPHICS::_PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_FLOAT(param);
	}
	void Scaleform::pushParam(const bool& param) const
	{
		GRAPHICS::_PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_BOOL(param);
	}

	void Scaleform::finishFunction() const
	{
		GRAPHICS::_POP_SCALEFORM_MOVIE_FUNCTION_VOID();
	}

	void Scaleform::render2DFullScreen() const
	{
		GRAPHICS::DRAW_SCALEFORM_MOVIE_FULLSCREEN(this->m_handle, 255, 255, 255, 255, 0);
	}
	void Scaleform::render2D(const Vec2& pos, const Vec2& size) const
	{
		GRAPHICS::DRAW_SCALEFORM_MOVIE(this->m_handle, pos.x, pos.y, size.x, size.y, 255, 255, 255, 255, 0);
	}
}