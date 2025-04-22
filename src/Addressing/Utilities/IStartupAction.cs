namespace Addressing.Utilities
{
    /// <summary>
    /// Represents an action that should be executed after the service provider is built.
    /// This is a workaround for scenarios where we need to modify services after they are resolved.
    /// </summary>
    public interface IStartupAction
    {
        /// <summary>
        /// Executes the startup action.
        /// </summary>
        void Execute();
    }
}
