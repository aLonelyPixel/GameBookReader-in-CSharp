using System.Collections.Generic;

namespace GameBook.Domain
{
    public class Paragraph
    {
        public Paragraph(int index, string text, IEnumerable<Choice> choices)
        {
            Index = index;
            Text = text;
            foreach (var choice in choices)
            {
                Choices.Add(choice);
            }
        }

        public string Text { get; }

        public IList<Choice> Choices { get; } = new List<Choice>();

        public int Index { get; }

        public bool IsTerminal() => Choices.Count == 0;

        public int GetChoiceDestination(in int choiceIndex) 
            => choiceIndex >= 0 && choiceIndex < Choices.Count ? Choices[choiceIndex].DestParagraph : -1;

        public IDictionary<string, int> GetChoices()
        {
            IDictionary<string, int> choicesDictionary = new Dictionary<string, int>();
            foreach (var choice in Choices)
            {
                choicesDictionary.Add(choice.Text, choice.DestParagraph);
            }
            return choicesDictionary;
        }

        public string GetLabel()
        {
            var label = "";
            var firstWords = Text.Split(' ');
            for (var i = 0; i < 4; i++)
            {
                if (i < firstWords.Length)
                {
                    label += firstWords[i] + " ";
                }
            }
            return $"{label}...";
        }
    }
}