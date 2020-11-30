using System.Collections.Generic;

namespace GameBook.Domain
{
    public class ReadingSession : IReadingSession
    {
        private readonly IBook _myBook;
        private int _currentParagraph;
        private readonly IList<int> _visitedParagraphs = new List<int>();
        private IDictionary<string, int> _choices;
        public string WarningMessage { get; set; }

        public ReadingSession(IBook book)
        { 
            _myBook = book;
            _currentParagraph = 1; 
            _visitedParagraphs.Add(_currentParagraph);
            _choices = book.GetParagraphChoices(_currentParagraph);
            WarningMessage = "No message";
        }

        public virtual string GetBookTitle() => _myBook.Name;

        public int GetCurrentParagraph() => _currentParagraph;

        public string GetParagraphText(int paragraphIndex) => _myBook.GetParagraphText(paragraphIndex);

        public IDictionary<string, int> GetParagraphChoices(int paragraphIndex) => _choices = _myBook.GetParagraphChoices(paragraphIndex);

        public void GoToParagraphByChoice(int destinationParagraph)
        {
            _currentParagraph = destinationParagraph;
            if (_visitedParagraphs.Contains(_currentParagraph))
            {
                _visitedParagraphs.Add(_currentParagraph);
                WarningMessage = $"Vous avez déjà lu le paragraphe {_currentParagraph}. Vous êtes ensuite allé au paragraphe {_visitedParagraphs[_visitedParagraphs.IndexOf(_currentParagraph) + 1]}";
            }
            _visitedParagraphs.Add(_currentParagraph);
            WarningMessage = $"Vous quittez le paragraphe {_visitedParagraphs[^2]} pour aller au paragraphe {_visitedParagraphs[^1]}";
            if (HasStoryEnded())
            {
                WarningMessage += ". Vous avez atteint la fin du livre.";
            }
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
            WarningMessage = "No message";
        }

        public void GoToVisitedParagraph(string paragraphText)
        {
            _currentParagraph = _myBook.GetParagraphIndex(paragraphText);
            AdjustVisitedParagraphs(_currentParagraph);
        }

        public string GetParagraphContent() => _myBook.GetParagraphText(_currentParagraph); 

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