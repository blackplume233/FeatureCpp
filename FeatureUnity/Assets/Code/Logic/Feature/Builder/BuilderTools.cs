using UnityEngine;

namespace Code.Logic.Feature
{
    public static  class BuilderTools
    {
        public static void VecMax(ref Vector3 a, ref Vector3 b)
        {
            a.x = Mathf.Max(a.x, b.x);
            a.y = Mathf.Max(a.y, b.y);
            a.z = Mathf.Max(a.z, b.z);
        }
        
        public static void VecMin(ref Vector3 a, ref Vector3 b)
        {
            a.x = Mathf.Min(a.x, b.x);
            a.y = Mathf.Min(a.y, b.y);
            a.z = Mathf.Min(a.z, b.z);
        }
        
        public static unsafe void VecCopy(float* dst, Vector3 src)
        {
            dst[0] = src.x;
            dst[1] = src.y;
            dst[2] = src.z;
        }
        
        public static unsafe void VecCopy(float* dst, float* src)
        {
            dst[0] = src[0];
            dst[1] = src[1];
            dst[2] = src[2];
        }

        public static unsafe void Swap<T>(ref T a, ref T b)
        {
            (a, b) = (b, a);
        }
        public static unsafe void Swap(ref float* a, ref float* b)
        {
            float* tmp = a;
            a = b;
            b = tmp;
        }
    }
}