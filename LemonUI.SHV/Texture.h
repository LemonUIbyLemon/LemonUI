#pragma once
#include "Vectors.h"

#include <string>

namespace LemonUI
{
	class Texture
	{
	public:
		Texture(const char* dict, const char* name);
		Texture(const std::string& dict, const std::string& name);
		Texture() = default;

		const char*& getDict() const { return (const char*&)*&this->m_dict; };
		const char*& getName() const { return (const char*&)*&this->m_name; };
		Vec2 getSize() const;

		void set(const char* dict, const char* name);
		void set(const std::string& dict, const std::string& name);

		void ensureLoaded() const;

		void render(const Vec2& pos) const;
		void render(const Vec2& pos, const Vec2& size) const;
		void render(const Vec2& pos, const Vec2& size, const float& heading) const;
		void render(const Vec2& pos, const Vec2& size, const float& heading, const Vec4& color) const;

	private:
		char* m_dict = nullptr;
		char* m_name = nullptr;
	};
}