cmake_minimum_required(VERSION 3.25)

project(FeatureCpp)

set(CMAKE_CXX_STANDARD 20)
add_executable(FeatureCpp  main.cpp
        UnitTest/ReturnTest.cpp
        UnitTest/ReturnTest.h)
