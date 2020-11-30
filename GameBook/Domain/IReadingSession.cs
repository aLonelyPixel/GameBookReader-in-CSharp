using System.Collections.Generic;

namespace GameBook.Domain
{
    public interface IReadingSession
    {
        public string WarningMessage { get; set; }
        public string GetBookTitle();
        public int GetCurrentParagraph();
        public string GetParagraphText(int paragraphIndex);
        public IDictionary<string, int> GetParagraphChoices(int paragraphIndex);
        public void GoToParagraphByChoice(int choiceIndex);
        public bool HasStoryEnded();
        public IEnumerable<string> GetVisitedParagraphs();
        public void GoBackToPrevious();
        public void GoToVisitedParagraph(string paragraphText);
        public string GetParagraphContent();
    }
}