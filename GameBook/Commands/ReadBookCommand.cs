using System;
using System.Collections.Generic;
using GameBook.Domain;

namespace GameBook.Commands
{
    public class ReadBookCommand : ICommands
    {
        private readonly MainPresentationModel _mpModel;

        public ReadBookCommand(MainPresentationModel mpModel) =>_mpModel = mpModel;

        public void Execute()
        {
           Console.WriteLine(_mpModel.GetBookTitle());
           string userChoice;
           do
           {
               Console.Write("Continuer ? [o/n] ");
               userChoice = Console.ReadLine();
               if (userChoice == null || !userChoice.Equals("o")) continue;
               ReadBook();
               _mpModel.ClearHistory();
           } while (string.IsNullOrEmpty(userChoice));
        }

        private void ReadBook()
        {
            var currentParagraphIndex = 1;
            var chosen = "";
            var hasEnded = false;

            while (!hasEnded)
            {
                var currentParagraph = _mpModel.GetParagraph(currentParagraphIndex);
                Console.WriteLine("\nParagraphe " + currentParagraph.Index + ":\n" + currentParagraph.Text + "\n");
                if (chosen != null && chosen.ToLower() != "r")
                {
                    _mpModel.AddReadParagraph(currentParagraphIndex);
                }
                
                var choices = currentParagraph.Choices;
                var choicesDictionary = new Dictionary<int, Choice>();
                var choiceCounter = 1;
                foreach (var choice in choices)
                {
                    Console.WriteLine(choiceCounter + ".\t" + choice.Text);
                    choicesDictionary.Add(choiceCounter, choice);
                    choiceCounter++;
                }

                Console.Write(currentParagraph.IsTerminal()
                    ? "Le livre est terminé ! Encodez 'q' pour revenir au menu ou 'r' pour revenir au paragraphe précédent : "
                    : "Choisissez une option ou revenez au paragraphe précédent en encodant 'r' : ");

                chosen = Console.ReadLine();

                if (chosen != null && chosen.ToLower() == "q")
                {
                    hasEnded = true;
                    continue;
                }

                if (chosen != null && chosen.ToLower() == "r")
                {
                    currentParagraphIndex = GoBack();
                    continue;
                }

                var optionValue = CheckOption(chosen);

                if (optionValue == 0) continue;
                
                if (choicesDictionary.ContainsKey(optionValue))
                {
                    currentParagraphIndex = choicesDictionary[optionValue].DestParagraph;
                    if (_mpModel.ContainsParagraph(currentParagraphIndex)) continue;
                    Console.WriteLine("Le paragraphe n'existe pas ! ");
                    break;
                }
                else
                {
                    Console.Write("Entrez un choix valide !");
                }
            }
        }

        private int GoBack() =>_mpModel.GetPreviousParagraph() == -1 ? 1 : _mpModel.GetPreviousParagraph();

        private static int CheckOption(string chosen)
        {
            if (chosen.ToLower() == "r")
            {
                return 0;
            }
            if ((string.IsNullOrEmpty(chosen) || string.IsNullOrWhiteSpace(chosen)))
            {
                return -1;
            }
            return Convert.ToInt32(chosen);
        }
    }
}