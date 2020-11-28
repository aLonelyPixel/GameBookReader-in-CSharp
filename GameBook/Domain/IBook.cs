using System.Collections.Generic;

namespace GameBook.Domain
{
    public interface IBook
    {
        public string Name { get; }
        public string GetParagraphText(int paragraphIndex);
        public IEnumerable<string> GetParagraphChoices(int paragraphIndex);
        public int GetChoiceDestination(int currentParagraph, int choiceIndex);
        public IEnumerable<string> GetParagraphsLabels(IEnumerable<int> visitedParagraphs);
        public bool ParagraphIsFinal(int currentParagraph);
        public int GetParagraphIndex(string paragraphText);
    }
}