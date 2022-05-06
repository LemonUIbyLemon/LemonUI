#include "pch.h"
#include "Texture.h"
#include "Helper.h"

#include <shv/natives.h>

namespace LemonUI
{
	Texture::Texture(const char* dict, const char* name) : m_dict{ const_cast<char*>(dict) }, m_name{ const_cast<char*>(name) }
	{}
	Texture::Texture(const std::string& dict, const std::string& name) : m_dict{ const_cast<char*>(dict.c_str()) }, m_name{ const_cast<char*>(name.c_str()) }
	{}

	Vec2 Texture::getSize() const
	{
		Vector3 result = GRAPHICS::GET_TEXTURE_RESOLUTION(this->m_dict, this->m_name);
		return { result.x, result.y };
	}

	void Texture::set(const char* dict, const char* name)
	{
		this->m_dict = const_cast<char*>(dict);
		this->m_name = const_cast<char*>(name);

		this->ensureLoaded();
	}
	void Texture::set(const std::string& dict, const std::string& name)
	{
		this->set(dict.c_str(), name.c_str());
	}

	void Texture::ensureLoaded() const
	{
		if (!GRAPHICS::HAS_STREAMED_TEXTURE_DICT_LOADED(this->m_dict))
		{
			GRAPHICS::REQUEST_STREAMED_TEXTURE_DICT(this->m_dict, 1);
		}
	}

	void Texture::render(const Vec2& pos) const
	{
		this->render(pos, this->getSize(), 0.0f, {255.0f});
	}
	void Texture::render(const Vec2& pos, const Vec2& size) const
	{
		this->render(pos, size, 0.0f, { 255.0f });
	}
	void Texture::render(const Vec2& pos, const Vec2& size, const float& heading) const
	{
		this->render(pos, size, heading, { 255.0f });
	}
	void Texture::render(const Vec2& pos, const Vec2& size, const float& heading, const Vec4& color) const
	{
		this->ensureLoaded();

		Vec2 spos = getScreenScale(getRectCenter(pos, size));
		Vec2 ssize = getScreenScale(size);

		GRAPHICS::DRAW_SPRITE(this->m_dict, this->m_name, spos.x, spos.y, ssize.x, ssize.y, heading, (int)(color.r * 255.0f), (int)(color.g * 255.0f), (int)(color.b * 255.0f), (int)(color.a * 255.0f));
	}
}