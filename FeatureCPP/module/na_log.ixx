module;
#include "iostream"
export module na_log;

export template<typename... Args>
void log_info(const char* format, const Args&... rest){
    std::printf(format,rest...);
}

