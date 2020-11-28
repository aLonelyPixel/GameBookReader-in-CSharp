using System.Collections.Generic;

namespace GameBook.Domain
{
    public interface IReadingSession
    {
        public string GetBookTitle();
        public int GetCurrentParagraph();
        public string GetParagraphText(int paragraphIndex);
        public IEnumerable<string> GetParagraphChoices(int paragraphIndex);
        public string GoToParagraphByChoice(int choiceIndex);
        public bool HasStoryEnded();
        public IEnumerable<string> GetVisitedParagraphs();
        public void GoBackToPrevious();
        public void GoToVisitedParagraph(string paragraphText);
        public string GetParagraphContent();
    }
}