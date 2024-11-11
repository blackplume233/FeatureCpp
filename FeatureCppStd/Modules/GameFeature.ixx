// //
// // Created by muyu li on 2023/8/23.
// //
// #include "list"
// #include "array"
// export module GameFeature;
//
// enum EIndexType{
//     CommonPtr
// };
//
// template<enum EIndexType type>
// struct FGlobalIndexCreator{
// public :
//     FGlobalIndexCreator(){
//         Index = IncrementIndex();
//     }
//     uint32_t Idx(){
//         return Index;
//     }
// private:
//     FGlobalIndexCreator(const FGlobalIndexCreator& other){
//         Index = 0;
//     }
//     static uint32_t IncrementIndex();
//
// protected:
//     uint32_t Index;
// };
//
// template<enum EIndexType type>
// uint32_t FGlobalIndexCreator<type>::IncrementIndex() {
//     static uint32_t StaticIndex = 0;
//     return ++StaticIndex;
// }
//
// export class FContext{
//
// public:
//     template<class Type>
//     void RegisterObj(Type* Obj);
//
//     template<class Type>
//             uint32_t GetGlobalIndex();
//
//     template<class Type>
//             Type* GetObj();
// private:
//     std::vector<void*> objects;
// };
//
// template<class Type>
// Type *FContext::GetObj() {
//     auto index = GetGlobalIndex<Type>();
//     assert(objects.size() > index && objects[index] != nullptr);
//     if(objects.size() > index){
//         return  static_cast<Type*>(objects[index]);
//     }
//     return nullptr;
// };
//
// template<class Type>
// uint32_t FContext::GetGlobalIndex() {
//     static FGlobalIndexCreator<CommonPtr> Index{};
//     return Index.Idx();
// };
//
// template<class Type>
// void FContext::RegisterObj(Type *Obj) {
//     auto index = GetGlobalIndex<Type>();
//     while (objects.size() <= index){
//         objects.resize(objects.size() * 2);
//     }
//     objects[index] = static_cast<void*>(Obj);
// };
//
