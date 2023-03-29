//
// Created by muyu li on 2023/3/28.
//
#include "iostream"
export module na_log;
export namespace na::log{
    template<typename... Args>
    void LogInfo(const char* format, const Args&... rest){
        std::printf(format,rest...);
    }
}
