using Ludiq;
using UnityEngine;

namespace Bolt
{
    [UnitCategory("Events/Physics")]
    public abstract class CollisionEventUnit : GameObjectEventUnit<Collision>
    {
        /// <summary>
        /// The collider we hit.
        /// </summary>
        [DoNotSerialize]
        public ValueOutput collider { get; private set; }

        /// <summary>
        /// The contact points generated by the physics engine.
        /// </summary>
        [DoNotSerialize]
        public ValueOutput contacts { get; private set; }

        /// <summary>
        /// The total impulse applied to this contact pair to resolve the collision.
        /// </summary>
        [DoNotSerialize]
        public ValueOutput impulse { get; private set; }

        /// <summary>
        /// The relative linear velocity of the two colliding objects.
        /// </summary>
        [DoNotSerialize]
        public ValueOutput relativeVelocity { get; private set; }

        /// <summary>
        /// The complete collision data object.
        /// </summary>
        [DoNotSerialize]
        public ValueOutput data { get; private set; }

        protected override void Definition()
        {
            base.Definition();

            collider = ValueOutput<Collider>(nameof(collider));
            contacts = ValueOutput<ContactPoint[]>(nameof(contacts));
            impulse = ValueOutput<Vector3>(nameof(impulse));
            relativeVelocity = ValueOutput<Vector3>(nameof(relativeVelocity));
            data = ValueOutput<Collision>(nameof(data));
        }

        protected override void AssignArguments(Flow flow, Collision collision)
        {
            flow.SetValue(collider, collision.collider);
            flow.SetValue(contacts, collision.contacts);
            flow.SetValue(impulse, collision.impulse);
            flow.SetValue(relativeVelocity, collision.relativeVelocity);
            flow.SetValue(data, collision);
        }
    }
}