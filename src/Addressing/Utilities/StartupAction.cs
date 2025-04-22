using System;

namespace Addressing.Utilities
{
    /// <summary>
    /// A concrete implementation of IStartupAction that wraps a delegate.
    /// </summary>
    public class StartupAction : IStartupAction
    {
        private readonly Action _action;

        /// <summary>
        /// Initializes a new instance of the StartupAction class.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public StartupAction(Action action)
        {
            _action = action;
        }

        /// <summary>
        /// Executes the wrapped action.
        /// </summary>
        public void Execute()
        {
            _action();
        }
    }
}
