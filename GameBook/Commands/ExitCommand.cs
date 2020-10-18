using System;

namespace GameBook.Commands
{
    public class ExitCommand : ICommands
    {
        public void Execute()
        {
            Console.WriteLine("Au revoir ! ");
            Environment.Exit(0);
        }
    }
}