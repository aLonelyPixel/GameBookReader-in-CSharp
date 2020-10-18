using System;
using GameBook.Commands;

namespace GameBook.Terminal
{
    public class Terminal
    {
        private readonly ExitCommand _exitCommand;
        private readonly ReadBookCommand _readBookCommand;

        public Terminal(ExitCommand exitCommand, ReadBookCommand readBookCommand)
        {
            _exitCommand = exitCommand;
            _readBookCommand = readBookCommand;
        }

        public void Loop()
        {
            string userChoice;
            do
            {
                Console.WriteLine("\nMENU\n\n[1] Lire le livre\n[2] Quitter");
                userChoice = Console.ReadLine();
                switch (userChoice?.ToLower() ?? string.Empty)
                {
                    case "1":
                        _readBookCommand.Execute();
                        break;
                    case "2":
                        break;
                    default:
                        Console.WriteLine("Entre une commande existante idiot :/");
                        break;
                }
            } while (userChoice != null && !userChoice.Equals("2"));
            _exitCommand.Execute();
        }
    }
}