using System.Linq;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Ludiq
{
    public static class AssetBundleUtility
    {
        public static bool IsLoaded(this AssetBundle bundle)
        {
            return AssetBundle.GetAllLoadedAssetBundles().Any(b => b == bundle);
        }
    }
}
