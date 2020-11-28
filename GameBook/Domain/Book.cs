using System.Collections.Generic;

namespace GameBook.Domain
{
    public class Book : IBook
    {
        public Book(string bookName, params Paragraph[] newParagraphs)
        {
            Name = bookName;
            foreach (var paragraphs in newParagraphs)
            {
                Paragraphs.Add(paragraphs.Index, paragraphs);
            }
        }

        public string Name { get; }
        public Dictionary<int, Paragraph> Paragraphs { get; } = new Dictionary<int, Paragraph>();

        public bool ContainsParagraph(int paragraphIndex) => Paragraphs.ContainsKey(paragraphIndex);
        
        public Paragraph GetParagraph(int paragraphIndex) => Paragraphs[paragraphIndex];

        public string GetParagraphText(int paragraphIndex) => Paragraphs[paragraphIndex].Text;

        public IEnumerable<string> GetParagraphChoices(int paragraphIndex) => Paragraphs[paragraphIndex].GetChoices();

        public int GetChoiceDestination(int currentParagraph, int choiceIndex) =>
            Paragraphs[currentParagraph].GetChoiceDestination(choiceIndex);

        public bool ParagraphIsFinal(int currentParagraph) => Paragraphs[currentParagraph].IsTerminal();

        public IEnumerable<string> GetParagraphsLabels(IEnumerable<int> visitedParagraphs)
        {
            IList<string> paragraphLabels = new List<string>();
            foreach (var paragraph in visitedParagraphs)
            {
                paragraphLabels.Add((Paragraphs[paragraph].GetLabel()));
            }

            return paragraphLabels;
        }

        public int GetParagraphIndex(string paragraphText)
        {
            paragraphText = paragraphText.Substring(0, paragraphText.Length - 5);
            foreach (var paragraph in Paragraphs.Values)
            {
                if (paragraph.Text.StartsWith(paragraphText))
                {
                    return paragraph.Index;
                }
            }
            return -1;
        }
    }
}