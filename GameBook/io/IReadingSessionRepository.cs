using System.Collections.Generic;

namespace GameBook.io
{
    public interface IReadingSessionRepository
    {
        public void Save(string bookTitle, IList<int> visitedParagraphs, string bookPath);

        public IList<int> Open(string bookTitle);

        public string OpenLastSession();
    }
}