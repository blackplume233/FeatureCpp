
export module Nalog;
#include "iostream"
export namespace Log {
    template<typename... Args>
    void LogInfo(const char* format, const Args&... rest){
        std::printf(format,rest...);
    }
    void Debug() {
        
    }
}
