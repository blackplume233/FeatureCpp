using UnityEngine;

namespace Ludiq
{
    // The dependency must always implement the receiver,
    // because it must always notify the dependency manager of its deserialization.
    public interface ISerializationDependency : ISerializationCallbackReceiver { }
}