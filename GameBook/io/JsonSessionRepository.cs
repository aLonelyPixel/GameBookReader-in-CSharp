using System.Collections.Generic;
using System.IO;
using GameBook.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GameBook.io
{
    public class JsonSessionRepository : IReadingSessionRepository
    {
        string _sessionFilePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.FullName + $"\\readingSession.json";

        public string GetLastBookRead()
        {
            using StreamReader file = File.OpenText(_sessionFilePath);
            using JsonTextReader reader = new JsonTextReader(file);
            JObject mainJObject = (JObject) JToken.ReadFrom(reader);
            return (string) mainJObject["LastBookPath"];
        }

        public void Save(IReadingSession readingSession, string path) //TODO doesnt need the whole reading session, adapt parameters
        {
            JObject newSessions =
                new JObject(
                   // new JProperty("LastBookPath", ""),
                    new JProperty("booksSaved",
                        new JArray(
                            new JObject(
                                new JProperty(readingSession.GetBookTitle(),
                                    new JObject((
                                        new JProperty("visitedParagraphs",
                                            new JArray(readingSession.GetVisitedParagraphs())))))))));

            if (!File.Exists(path))
            {
                File.WriteAllText(path, newSessions.ToString());
            }
            else
            {
                var sameBook = false;
                JObject newBookSession = new JObject(
                    new JProperty(readingSession.GetBookTitle(),
                        new JObject(
                            new JProperty("visitedParagraphs",
                                new JArray(readingSession.GetVisitedParagraphs())))));

                using (StreamReader file = File.OpenText(path))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject mainObject = (JObject) JToken.ReadFrom(reader);
                    foreach (var bookCollection in mainObject["booksSaved"])
                    {
                        foreach (KeyValuePair<string, JToken> book in (JObject) bookCollection)
                        {
                            if (book.Key == readingSession.GetBookTitle())
                            {
                                sameBook = true;
                            }
                        }
                    }
                }

                if (sameBook)
                {
                    File.WriteAllText(path, newSessions.ToString());
                }
                else
                {
                    JObject oldSession = JObject.Parse(File.ReadAllText(path));
                    JArray bookCollection = (JArray)oldSession["booksSaved"];
                    bookCollection?.Add(newBookSession);
                    var updatedSessions = new JObject(
                        new JProperty("booksSaved", bookCollection));
                    File.WriteAllText(path, updatedSessions.ToString());
                }
            }
        }

        public IList<int> Open(string bookTitle, string path)
        {
            if (File.Exists(path))
            {
                IList<int> lastSessionInfo = new List<int>();

                using StreamReader file = File.OpenText(path);
                using JsonTextReader reader = new JsonTextReader(file);
                JObject mainObject = (JObject)JToken.ReadFrom(reader);
                foreach (var bookCollection in mainObject["booksSaved"])
                {
                    foreach (KeyValuePair<string, JToken> book in (JObject)bookCollection)
                    {
                        if (book.Key == bookTitle)
                        {
                            lastSessionInfo = (book.Value?["visitedParagraphs"]?.ToObject<List<int>>());
                        }
                    }
                }
                return lastSessionInfo;
            }
            return null;
        }
    }
}