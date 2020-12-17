using System.Collections.Generic;

namespace GameBook.Domain
{
    public interface IReadingSession
    {
        public string WarningMessage { get; set; }
        string Path { get; set; }
        public string GetBookTitle();
        public int GetCurrentParagraph();
        public IDictionary<string, int> GetParagraphChoices(int paragraphIndex);
        public void GoToParagraphByChoice(int choiceIndex);
        public IDictionary<int, string> GetHistory();
        public IList<int> GetVisitedParagraphs();
        public void GoBackToPrevious();
        public string GetParagraphContent();
        public void GoToVisitedParagraph(int paragraphIndex);
        void OpenLastSession(IList<int> lastSession);
        void SetBook(IBook book, string path);
        bool IsFakeBook();
    }
}