#include <iostream>

#include "UnitTest/ReturnTest.h"

class Data{
public:
    int value = 2;
};

class Data2{
public:
    int value = 2;
};

int main() {
    ReturnTest test;
    std::cout << test.GetTemp();
    std::cout << "Finish";
}


int TestModule()
{
    // FContext Context;
    // Context.RegisterObj<Data>(new Data());
    // Context.RegisterObj<Data2>(new Data2());
    // std::cout << "Hello, World!" << std::endl;
    // Context.GetObj<Data2>()->value = 10;
    // std::cout << Context.GetObj<Data2>()->value << std::endl;
    // return 0;
    return 0;
}