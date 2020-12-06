using System.Collections.Generic;
using GameBook.Domain;

namespace GameBook.io
{
    public interface IReadingSessionRepository
    {
        public void Save(IReadingSession readingSession, string path);

        public IList<int> Open(string bookTitle, string path);
    }
}