using UnityObject = UnityEngine.Object;

namespace Ludiq
{
    public interface IGraphParent
    {
        IGraph childGraph { get; }

        bool isSerializationRoot { get; }

        UnityObject serializedObject { get; }

        IGraph DefaultGraph();
    }
}
