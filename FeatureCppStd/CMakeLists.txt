cmake_minimum_required(VERSION 3.25)

project(FeatureCpp)

set(CMAKE_CXX_STANDARD 20)
add_module(GameFeature Modules/GameFeature.ixx)
add_executable(FeatureCpp  Modules/GameFeature.ixx main.cpp)
