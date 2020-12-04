using System.Collections.Generic;
using GameBook.Domain;

namespace GameBook.io
{
    public interface IReadingSessionRepository
    {
        public void Save(IReadingSession readingSession);

        public IList<int>[] Open(IReadingSession readingSession);
    }
}