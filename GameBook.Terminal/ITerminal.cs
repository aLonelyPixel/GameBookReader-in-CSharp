using System;

namespace GameBook.Terminal
{
    public class ITerminal
    {
        public void Loop()
        {
            var userChoice = string.Empty;

            do
            {
                Console.WriteLine("\nWould you like to start [read]ing or [exit]?");
                userChoice = Console.ReadLine();
                switch (userChoice?.ToLower() ?? string.Empty)
                {
                    case "exit":
                        Environment.Exit(0);
                        break;
                    case "read":
                        //throw new NotImplementedException();
                        Console.WriteLine("[NOT IMPLEMENTED]");
                        break;
                    default:
                        Console.WriteLine("Please enter a valid command you muppet");
                        break;
                }
            } while (!userChoice.ToLower().Equals("exit"));
        }
    }
}