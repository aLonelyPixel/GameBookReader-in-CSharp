using System.Collections.Generic;
using GameBook.Domain;

namespace GameBook.MainPresentationModel
{
    public class MainPresentationModel
    {
        private readonly ReadingSession _readingSession;

        public MainPresentationModel(ReadingSession readingSession)
        {
            _readingSession = readingSession;
        }

        public string GetBookTitle() => _readingSession.GetBookTitle();

        public int GetCurrentParagraph() => _readingSession.GetCurrentParagraph();

        public string GetParagraphText(int paragraphIndex) => _readingSession.GetParagraphText(paragraphIndex);

        public IEnumerable<string> GetParagraphChoices(int paragraphIndex) => _readingSession.GetParagraphChoices(paragraphIndex);

        public string GoToParagraphByChoice(int choiceIndex) => _readingSession.GoToParagraphByChoice(choiceIndex);

        public bool StoryEnded() => _readingSession.HasStoryEnded();

        public IEnumerable<string> GetVisitedParagraphs() => _readingSession.GetVisitedParagraphs();

        public void GoBackToPrevious() => _readingSession.GoBackToPrevious();

        public void GoToVisitedParagraph(string paragraphText) => _readingSession.GoToVisitedParagraph(paragraphText);

    }
}
