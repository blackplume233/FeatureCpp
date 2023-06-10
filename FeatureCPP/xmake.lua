local is_mac = is_plat("macosx")
local is_linux = is_plat("linux")
local is_win = is_plat("windows")

target("FeatureCPP")
    set_kind("binary")
    set_languages("cxx20")
    add_cxxflags("/c")
    add_files("src/**.cpp")
    --add_files("module/**.ixx")
    add_defines("_DEBUG")


    --IMGUI
    add_includedirs("3rd/imgui")
    add_includedirs("3rd/imgui/backends")
    add_files("3rd/imgui/**.cpp")

    if (is_win) then
        add_includedirs("$(env VULKAN_SDK)/include")
        add_includedirs("$(env SDL2_DIR)")
    end
    add_linkdirs("$(env VULKAN_SDK)/Lib")
    add_links("vulkan-1","SDL2", "SDL2main", "libcpmtd")


    if (is_mac) then
        add_cxxflags("-std=c++20")
        add_cxxflags("-fmodules-ts")
        set_toolset("cxx", "clang")
    end

    

--
-- If you want to known more usage about xmake, please see https://xmake.io
--
-- ## FAQ
--
-- You can enter the project directory firstly before building project.
--
--   $ cd projectdir
--
-- 1. How to build project?
--
--   $ xmake
--
-- 2. How to configure project?
--
--   $ xmake f -p [macosx|linux|iphoneos ..] -a [x86_64|i386|arm64 ..] -m [debug|release]
--
-- 3. Where is the build output directory?
--
--   The default output directory is `./build` and you can configure the output directory.
--
--   $ xmake f -o outputdir
--   $ xmake
--
-- 4. How to run and debug target after building project?
--
--   $ xmake run [targetname]
--   $ xmake run -d [targetname]
--
-- 5. How to install target to the system directory or other output directory?
--
--   $ xmake install
--   $ xmake install -o installdir
--
-- 6. Add some frequently-used compilation flags in xmake.lua
--
-- @code
--    -- add debug and release modes
--    add_rules("mode.debug", "mode.release")
--
--    -- add macro defination
--    add_defines("NDEBUG", "_GNU_SOURCE=1")
--
--    -- set warning all as error
--    set_warnings("all", "error")
--
--    -- set language: c99, c++11
--    set_languages("c99", "c++11")
--
--    -- set optimization: none, faster, fastest, smallest
--    set_optimize("fastest")
--
--    -- add include search directories
--    add_includedirs("/usr/include", "/usr/local/include")
--
--    -- add link libraries and search directories
--    add_links("tbox")
--    add_linkdirs("/usr/local/lib", "/usr/lib")
--
--    -- add system link libraries
--    add_syslinks("z", "pthread")
--
--    -- add compilation and link flags
--    add_cxflags("-stdnolib", "-fno-strict-aliasing")
--    add_ldflags("-L/usr/local/lib", "-lpthread", {force = true})
--
-- @endcode
--

