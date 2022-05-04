#pragma once

#define WIN32_LEAN_AND_MEAN // Selten verwendete Komponenten aus Windows-Headern ausschließen

#include <string>
#include <memory>

#define CHECK_FORMAT_TYPES(type) typeid(type) == typeid(T)