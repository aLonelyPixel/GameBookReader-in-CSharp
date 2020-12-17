using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace GameBook.io
{
    public class JsonSessionRepository : IReadingSessionRepository
    {
        private readonly string _relativePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.FullName + $"\\readingSession.json";

        public void Save(string bookTitle, IList<int> visitedParagraphs)
        {
            if (File.Exists(_relativePath))
            {
                UpdateSaveFile(bookTitle, visitedParagraphs);
            }
            else
            {
                CreateSaveFile(bookTitle, visitedParagraphs);
            }
        }

        private void CreateSaveFile(string bookTitle, IList<int> visitedParagraphs)
        {
            JObject newSessions =
                new JObject(
                    new JProperty(bookTitle,
                        new JArray(visitedParagraphs)));
            File.WriteAllText(_relativePath, newSessions.ToString());
        }

        private void UpdateSaveFile(string bookTitle, IList<int> visitedParagraphs)
        {
            JObject oldSession = JObject.Parse(File.ReadAllText(_relativePath));
            if (oldSession.ContainsKey(bookTitle)) oldSession.Remove(bookTitle);
            oldSession.Add(new JProperty(bookTitle,
                new JArray(visitedParagraphs)));
            File.WriteAllText(_relativePath, oldSession.ToString());
        }

        public IList<int> Open(string bookTitle)
        {
            if (!File.Exists(_relativePath)) return null;
            var oldSession = JObject.Parse(File.ReadAllText(_relativePath));
            return oldSession.ContainsKey(bookTitle) ? oldSession[bookTitle]?.ToObject<List<int>>() : null;
        }
    }
}