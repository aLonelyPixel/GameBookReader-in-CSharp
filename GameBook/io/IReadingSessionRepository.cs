using System.Collections.Generic;
using GameBook.Domain;

namespace GameBook.io
{
    public interface IReadingSessionRepository
    {
        public void Save(string bookTitle, IList<int> visitedParagraphs);

        public IList<int> Open(string bookTitle);
    }
}