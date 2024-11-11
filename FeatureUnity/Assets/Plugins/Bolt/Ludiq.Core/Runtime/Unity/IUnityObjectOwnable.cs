using UnityObject = UnityEngine.Object;

namespace Ludiq
{
    public interface IUnityObjectOwnable
    {
        UnityObject owner { get; set; }
    }
}