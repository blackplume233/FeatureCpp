namespace Bolt
{
    /// <summary>
    /// Called when the application resumes.
    /// </summary>
    [UnitCategory("Events/Application")]
    public sealed class OnApplicationResume : GlobalEventUnit<EmptyEventArgs>
    {
        protected override string hookName => EventHooks.OnApplicationResume;
    }
}