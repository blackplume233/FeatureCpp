//
// Created by black on 2024/6/2.
//

#ifndef RETURNTEST_H
#define RETURNTEST_H
#include <algorithm>


class ReturnTest {
public:
    const int GetTemp()
    {
        return std::move(0);
    }
};



#endif //RETURNTEST_H
