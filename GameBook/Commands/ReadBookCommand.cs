using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using GameBook.Domain;

namespace GameBook.Commands
{
    public class ReadBookCommand : ICommands
    {
        private readonly MainPresentationModel _mpModel;
        private int _returnsNb = 1;
        private int _currentParagraphIndex = 1;

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
            var hasEnded = false;
            var chosen = "";
            while (!hasEnded)
            {
                if (_currentParagraphIndex == 1)
                {
                    _returnsNb = 1;
                }
                var currentParagraph = _mpModel.GetParagraph(_currentParagraphIndex);
                Console.WriteLine("\nParagraphe " + currentParagraph.Index + ":\n" + currentParagraph.Text + "\n");
                if (chosen != null && chosen.ToLower() != "r" && _currentParagraphIndex >= 1)
                {
                    _mpModel.AddReadParagraph(_currentParagraphIndex);
                }
                
                var choicesDictionary = InitChoices(currentParagraph);
                Console.Write(currentParagraph.IsTerminal()
                    ? "Le livre est terminé ! Encodez 'q' pour revenir au menu ou 'r' pour revenir au paragraphe précédent : "
                    : "Choisissez une option ou revenez au paragraphe précédent en encodant 'r' : ");
                chosen = Console.ReadLine();

                if (Quits(chosen, ref hasEnded)) continue;
                if (GoesBack(chosen, ref _currentParagraphIndex)) continue;

                var optionValue = CheckOption(chosen);
                if (optionValue == 0) continue;
                if (choicesDictionary.ContainsKey(optionValue))
                {
                    _currentParagraphIndex = choicesDictionary[optionValue].DestParagraph;
                    if (_mpModel.ContainsParagraph(_currentParagraphIndex)) continue;
                    Console.WriteLine("Le paragraphe n'existe pas ! ");
                    break;
                }
                Console.Write("Entrez un choix valide !");
            }
        }

        private bool GoesBack(string chosen, ref int currentParagraphIndex)
        {
            if (chosen == null || chosen.ToLower() != "r") return false;
            currentParagraphIndex = GoBack();
            return true;
        }

        private static bool Quits(string chosen, ref bool hasEnded)
        {
            if (chosen == null || chosen.ToLower() != "q") return false;
            hasEnded = true;
            return true;
        }

        private static Dictionary<int, Choice> InitChoices(Paragraph currentParagraph)
        {
            var choices = currentParagraph.Choices;
            var choicesDictionary = new Dictionary<int, Choice>();
            var choiceCounter = 1;
            foreach (var choice in choices)
            {
                Console.WriteLine(choiceCounter + ".\t" + choice.Text);
                choicesDictionary.Add(choiceCounter, choice);
                choiceCounter++;
            }
            return choicesDictionary;
        }

        /// <summary>
        /// Returns the previous paragraph if it exists. If requested at the very first
        /// paragraph, it sends back to the first paragraph
        /// </summary>
        /// <returns>The index of the previous paragraph</returns>
        private int GoBack()
        {
            _returnsNb++;
            if (_currentParagraphIndex == 1)
            {
                return 1;
            }
            var value = _mpModel.GetPreviousParagraph(_returnsNb);
            if (value == -1)
            {
                return 1;
            }
            return value;
        }

        /// <summary>
        /// Converts the entered string in a valid integer
        /// </summary>
        /// <param name="enteredText">The string to be converted</param>
        /// <returns>The equivalent integer or -1 if the text is null, empty, blank or not an integer</returns>
        private static int CheckOption(string enteredText) => !IsInteger(enteredText) ? -1 : Convert.ToInt32(enteredText);

        /// <summary>
        /// Using a regular expression, this method checks if a string matches the pattern of a text
        /// </summary>
        /// <param name="enteredText">The string to check</param>
        /// <returns>True if the string is an integer</returns>
        private static bool IsInteger(string enteredText)
        {
            var intPattern = new Regex(@"^\d$");
            return intPattern.IsMatch(enteredText);
        }
    }
}