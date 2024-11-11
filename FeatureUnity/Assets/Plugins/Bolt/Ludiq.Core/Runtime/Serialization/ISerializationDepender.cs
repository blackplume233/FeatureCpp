using System.Collections.Generic;
using UnityEngine;

namespace Ludiq
{
    public interface ISerializationDepender : ISerializationCallbackReceiver
    {
        IEnumerable<ISerializationDependency> deserializationDependencies { get; }

        void OnAfterDependenciesDeserialized();
    }
}