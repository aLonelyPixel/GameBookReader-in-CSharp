using System.Collections.Generic;

namespace GameBook.Domain
{
    public class ReadingSession
    {
        private readonly Book _myBook;
        private int _currentParagraph;
        private readonly IList<int> _visitedParagraphs = new List<int>();

        public ReadingSession(Book book)
        { 
            _myBook = book;
            _currentParagraph = 1; 
            _visitedParagraphs.Add(_currentParagraph);
        }

        public string GetBookTitle() => _myBook.Name;

        public int GetCurrentParagraph() => _currentParagraph;

        public string GetParagraphText(int paragraphIndex) => _myBook.GetParagraphText(paragraphIndex);

        public IEnumerable<string> GetParagraphChoices(int paragraphIndex) => _myBook.GetParagraphChoices(paragraphIndex);

        public string GoToParagraphByChoice(int choiceIndex)
        {
            _currentParagraph = _myBook.GetChoiceDestination(_currentParagraph, choiceIndex);
            if (_visitedParagraphs.Contains(_currentParagraph))
            {
                _visitedParagraphs.Add(_currentParagraph);
                return $"Vous avez déjà lu le paragraphe {_currentParagraph}. Vous êtes ensuite allé au paragraphe {_visitedParagraphs[_visitedParagraphs.IndexOf(_currentParagraph) + 1]}";
            }
            _visitedParagraphs.Add(_currentParagraph);
            return $"Vous quittez le paragraphe {_visitedParagraphs[^2]} pour aller au paragraphe {_visitedParagraphs[^1]}";
        }

        public IEnumerable<string> GetVisitedParagraphs()
        {
            IList<string> visitedParagraphs = new List<string>(_myBook.GetParagraphsLabels(_visitedParagraphs));
            return visitedParagraphs;
        }

        public bool HasStoryEnded() => _myBook.ParagraphIsFinal(_currentParagraph);

        public void GoBackToPrevious()
        {
            if (_visitedParagraphs.Count < 2) return;
            _visitedParagraphs.RemoveAt(_visitedParagraphs.Count-1);
            _currentParagraph = _visitedParagraphs[^1];
        }

        public void GoToVisitedParagraph(string paragraphText)
        {
            _currentParagraph = _myBook.GetParagraphIndex(paragraphText);
            AdjustVisitedParagraphs(_currentParagraph);
        }

        private void AdjustVisitedParagraphs(int currentParagraph)
        {
            for (var i = _visitedParagraphs.Count-1; i >= 0; i--)
            {
                if (_visitedParagraphs[i].Equals(currentParagraph)) return;
                _visitedParagraphs.RemoveAt(_visitedParagraphs.Count-1);
            }
        }
    }
}