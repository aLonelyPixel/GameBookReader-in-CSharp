using System.Collections.Generic;

namespace GameBook
{
    public class Paragraph
    {
        public Paragraph(int index, string text, params Choice[] newChoices)
        {
            Index = index;
            Text = text;
            foreach (var choice in newChoices)
            {
                Choices.Add(choice);
            }
        }

        public string Text { get; }

        public List<Choice> Choices { get; } = new List<Choice>();

        public int Index { get; }

        public bool IsTerminal() => Choices.Count == 0;
    }
}