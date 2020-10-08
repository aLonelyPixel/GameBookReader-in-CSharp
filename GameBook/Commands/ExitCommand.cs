using System;

namespace GameBook.Commands
{
    public class ExitCommand : ICommands
    {
        public void Execute()
        {
            Environment.Exit(0);
        }
    }
}