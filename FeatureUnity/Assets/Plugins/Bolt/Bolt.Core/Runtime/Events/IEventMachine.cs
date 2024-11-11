using Ludiq;
using UnityEngine;

namespace Bolt
{
    public interface IEventMachine : IMachine
    {
        void TriggerAnimationEvent(AnimationEvent animationEvent);
        void TriggerUnityEvent(string name);
    }
}
