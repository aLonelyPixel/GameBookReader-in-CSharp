using System;
using System.Collections.Generic;

namespace GameBook.Domain
{
    public class MainPresentationModel
    {
        private readonly Book _myBook;
        private readonly List<int> _gameProgress = new List<int>();

        public MainPresentationModel(Book myBook) => _myBook = myBook;

        public string GetBookTitle() => _myBook.Name;

        public Paragraph GetParagraph(int paragraphIndex) => ContainsParagraph(paragraphIndex) ? _myBook.GetParagraph(paragraphIndex) : null;

        public void AddReadParagraph(int paragraphIndex) => _gameProgress.Add(paragraphIndex);

        public void ClearHistory() => _gameProgress.Clear();

        public bool ContainsParagraph(int paragraphIndex) => _myBook.ContainsParagraph(paragraphIndex);

        public int GetPreviousParagraph(int amountOfReturns)
        {
            if (_gameProgress.Count == 1)
            {
                return _gameProgress[^1];
            }
            if (_gameProgress.Count > 1)
            {
                return _gameProgress[^amountOfReturns];
            }
            return -1;
        }

        
    }
}