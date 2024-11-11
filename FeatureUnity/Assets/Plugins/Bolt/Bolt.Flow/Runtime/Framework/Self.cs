using Ludiq;
using UnityEngine;

namespace Bolt
{
    /// <summary>
    /// Returns the current game object.
    /// </summary>
    [SpecialUnit]
    public sealed class Self : Unit
    {
        /// <summary>
        /// The current game object.
        /// </summary>
        [DoNotSerialize]
        [PortLabelHidden]
        public ValueOutput self { get; private set; }

        protected override void Definition()
        {
            self = ValueOutput(nameof(self), Result).PredictableIf(IsPredictable);
        }

        private GameObject Result(Flow flow)
        {
            return flow.stack.self;
        }

        private bool IsPredictable(Flow flow)
        {
            return flow.stack.self != null;
        }
    }
}