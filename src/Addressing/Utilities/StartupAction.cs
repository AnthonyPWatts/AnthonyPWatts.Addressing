using System;

namespace ISOCodex.Addressing.Utilities
{
    public class StartupAction : IStartupAction
    {
        private readonly Action _action;

        public StartupAction(Action action)
        {
            _action = action;
        }

        public void Execute()
        {
            _action();
        }
    }
}
