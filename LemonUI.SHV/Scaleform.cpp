#include "pch.h"
#include "Scaleform.h"

#include <shv/natives.h>

namespace LemonUI
{
	Scaleform::Scaleform(const std::string& id)
	{
		this->request(id.c_str());
	}
	Scaleform::Scaleform(const char* id)
	{
		this->request(id);
	}
	Scaleform::Scaleform()
	{}
	Scaleform::~Scaleform()
	{
		if (this->isValid())
		{
			GRAPHICS::SET_SCALEFORM_MOVIE_AS_NO_LONGER_NEEDED(&this->m_handle);
		}
	}

	void Scaleform::request(const char* id)
	{
		this->m_handle = GRAPHICS::REQUEST_SCALEFORM_MOVIE(const_cast<char*>(id));
	}
	void Scaleform::request(const std::string& id)
	{
		this->request(id.c_str());
	}

	bool Scaleform::isValid()
	{
		return this->m_handle != 0;
	}
	bool Scaleform::isLoaded()
	{
		return this->isValid() ? GRAPHICS::HAS_SCALEFORM_MOVIE_LOADED(m_handle) : 0;
	}

	void Scaleform::startFunction(const char* name)
	{
		if (this->isValid())
		{
			GRAPHICS::_PUSH_SCALEFORM_MOVIE_FUNCTION(this->m_handle, const_cast<char*>(name));
		}
	}
	void Scaleform::startFunction(const std::string& name)
	{
		this->startFunction(name.c_str());
	}

	void Scaleform::pushParam(const char* param)
	{
		if (!this->isValid())
		{
			return;
		}

		GRAPHICS::_PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_STRING(const_cast<char*>(param));
	}
	void Scaleform::pushParam(const std::string& param)
	{
		this->pushParam(param.c_str());
	}
	void Scaleform::pushParam(const int& param)
	{
		if (!this->isValid())
		{
			return;
		}

		GRAPHICS::_PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT(param);
	}
	void Scaleform::pushParam(const float& param)
	{
		if (!this->isValid())
		{
			return;
		}

		GRAPHICS::_PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_FLOAT(param);
	}
	void Scaleform::pushParam(const bool& param)
	{
		if (!this->isValid())
		{
			return;
		}

		GRAPHICS::_PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_BOOL(param);
	}

	void Scaleform::finishFunction()
	{
		if (this->isValid())
		{
			GRAPHICS::_POP_SCALEFORM_MOVIE_FUNCTION_VOID();
		}
	}

	void Scaleform::callFunction(const char* name)
	{
		if (this->isValid())
		{
			GRAPHICS::CALL_SCALEFORM_MOVIE_METHOD(this->m_handle, const_cast<char*>(name));
		}
	}
	void Scaleform::callFunction(const std::string& name)
	{
		this->callFunction(name.c_str());
	}

	void Scaleform::renderFullScreen()
	{
		if (this->isValid())
		{
			GRAPHICS::DRAW_SCALEFORM_MOVIE_FULLSCREEN(this->m_handle, 255, 255, 255, 255, 0);
		}
	}
	void Scaleform::render(const vec2& pos, const vec2& res)
	{
		if (this->isValid())
		{
			GRAPHICS::DRAW_SCALEFORM_MOVIE(this->m_handle, pos.x, pos.y, res.x, res.y, 255, 255, 255, 255, 0);
		}
	}
}