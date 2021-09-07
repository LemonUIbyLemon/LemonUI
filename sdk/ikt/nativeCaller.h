// https://github.com/E66666666/GTAVManualTransmission/blob/master/thirdparty/ScriptHookV_SDK/inc/nativeCaller.h

#pragma once

#include "main.h"
#include <utility>

template <typename T>
static inline void nativePush(T val)
{
    static_assert(sizeof(T) <= sizeof(UINT64), "Type is too large");
    UINT64 val64 = 0;
    *reinterpret_cast<T *>(&val64) = val;
    nativePush64(val64);
}

template <typename R>
static inline R invoke(UINT64 hash)
{
    nativeInit(hash);
    return *reinterpret_cast<R *>(nativeCall());
}

template <typename R, class ... Args>
static inline R invoke(UINT64 hash, Args&& ... args)
{
    nativeInit(hash);

    (nativePush(std::forward<Args>(args)), ...);

    return *reinterpret_cast<R*>(nativeCall());
}
