﻿namespace Bolt
{
    /// <summary>
    /// Called every frame after all update functions have been called.
    /// </summary>
    [UnitCategory("Events/Lifecycle")]
    [UnitOrder(5)]
    public sealed class LateUpdate : MachineEventUnit<EmptyEventArgs>
    {
        protected override string hookName => EventHooks.LateUpdate;
    }
}