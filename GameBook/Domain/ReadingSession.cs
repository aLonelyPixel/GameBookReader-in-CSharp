using System.Collections.Generic;

namespace GameBook.Domain
{
    public class ReadingSession : IReadingSession
    {
        private readonly IBook _myBook;
        private int _currentParagraph;
        private IList<int> _visitedParagraphs;
        private readonly IDictionary<int, string> _readingHistory;
        public string WarningMessage { get; set;}

        public ReadingSession(IBook book)
        { 
            _myBook = book;
            _currentParagraph = 1;
            _visitedParagraphs = new List<int> {_currentParagraph};
            _readingHistory = new SortedDictionary<int, string>();
            WarningMessage = "No message";
        }

        public string GetBookTitle() => _myBook.Name;

        public int GetCurrentParagraph() => _currentParagraph;

        public string GetParagraphContent() => _myBook.GetParagraphText(_currentParagraph);

        public IDictionary<string, int> GetParagraphChoices(int paragraphIndex) => _myBook.GetParagraphChoices(paragraphIndex);

        public void GoToParagraphByChoice(int destinationParagraph)
        {
            UpdateMessage(destinationParagraph, -1);
            _currentParagraph = destinationParagraph;
            _visitedParagraphs.Add(_currentParagraph);
            
            if (_myBook.ParagraphIsFinal(_currentParagraph)) WarningMessage += ". Vous avez atteint la fin du livre.";
            UpdateHistory();
        }

        public IDictionary<int, string> GetHistory() => _readingHistory;

        private void UpdateHistory()
        {
            foreach (var visitedParagraph in _visitedParagraphs)
            {
                if (!_readingHistory.ContainsKey(visitedParagraph))
                {
                    _readingHistory.Add(visitedParagraph, _myBook.GetParagraphLabel(visitedParagraph));
                }
            }
        }

        private void UpdateMessage(int destination, int previouslyChosen)
        {
            if (_visitedParagraphs.Contains(destination))
            {
                if (_visitedParagraphs[^1] == destination)
                {
                    if (previouslyChosen > 0)
                    {
                        WarningMessage = $"Vous avez déjà lu le paragraphe {destination}. Vous êtes ensuite allé au paragraphe {previouslyChosen}";
                    }
                }
                else
                {
                    WarningMessage = $"Vous avez déjà lu le paragraphe {destination}. Vous êtes ensuite allé au paragraphe {_visitedParagraphs[_visitedParagraphs.IndexOf(destination) + 1]}";
                }
            }
            else
            {
                WarningMessage = $"Vous quittez le paragraphe {_currentParagraph} pour aller au paragraphe {destination}";
            }
        }

        public IList<int> GetVisitedParagraphs() => _visitedParagraphs;

         public void GoBackToPrevious()
        {
            if (_visitedParagraphs.Count < 2) return;
            if (_readingHistory.ContainsKey(_visitedParagraphs[^1]))
            {
                _readingHistory.Remove(_visitedParagraphs[^1]);
            }

            var previouslyChosen = _visitedParagraphs[^1];
            _visitedParagraphs.RemoveAt(_visitedParagraphs.Count-1);
            UpdateMessage(_visitedParagraphs[^1], previouslyChosen);
            _currentParagraph = _visitedParagraphs[^1];
        }

        public void GoToVisitedParagraph(int paragraphIndex)
        {
            UpdateMessage(paragraphIndex, -1);
            _currentParagraph = paragraphIndex;
            AdjustVisitedParagraphs(_currentParagraph);
        }

        private void AdjustVisitedParagraphs(int currentParagraph)
        {
            for (var i = _visitedParagraphs.Count-1; i >= 0; i--)
            {
                if (_visitedParagraphs[i].Equals(currentParagraph)) return;
                if (_readingHistory.ContainsKey(_visitedParagraphs[^1]))
                {
                    _readingHistory.Remove(_visitedParagraphs[^1]);
                }
                _visitedParagraphs.RemoveAt(_visitedParagraphs.Count-1);
            }
        }

        public void SetCurrentParagraph(int last)
        {
            _currentParagraph = last;
        }

        public void SetVisitedParagraphs(IList<int> visitedParagraphs)
        {
            _visitedParagraphs = visitedParagraphs;
            UpdateHistory();
        }
    }
}