using System.Collections.Generic;
using System.IO;
using GameBook.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GameBook.io
{
    public class SaveReadingSession : IReadingSessionRepository
    {
        private IReadingSession _readingSession;
        public SaveReadingSession(IReadingSession readingSession)
        {
            _readingSession = readingSession;
        }
        
        public void Save(IReadingSession readingSession)
        {
            SaveSessions mySaveSessions = new SaveSessions();
            mySaveSessions.AddBookSession(readingSession.GetBookTitle(), readingSession.GetVisitedParagraphs());

            if (!File.Exists(@"C:\Users\andre\Desktop\readingSession.json"))
            {
                File.WriteAllText(@"C:\Users\andre\Desktop\readingSession.json", JsonConvert.SerializeObject(mySaveSessions));
            }
            else
            {
                using (StreamReader file = File.OpenText(@"C:\Users\andre\Desktop\readingSession.json"))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject mainObject = (JObject) JToken.ReadFrom(reader);
                    JToken myBooks = mainObject["_booksSaved"];
                    if (myBooks != null)
                        foreach (KeyValuePair<string, JToken> thisVar in (JObject) myBooks)
                        {
                            if (thisVar.Key == readingSession.GetBookTitle())
                            {
                                
                            }
                        }
                    
                }
                
                using (StreamWriter file = File.AppendText(@"C:\Users\andre\Desktop\readingSession.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, mySaveSessions);
                }
            }
        }

        public IList<int>[] Open(IReadingSession readingSession)
        {
            if (File.Exists(@"C:\Users\andre\Desktop\readingSession.json"))
            {
                IList<int>[] lastSessionInfo = new IList<int>[2];
                lastSessionInfo[0] = new List<int>();
                lastSessionInfo[1] = new List<int>();
                using (StreamReader file = File.OpenText(@"C:\Users\andre\Desktop\readingSession.json"))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject mainObject = (JObject)JToken.ReadFrom(reader);
                    JToken myBooks = mainObject["_booksSaved"];
                    if (myBooks != null)
                        foreach (KeyValuePair<string, JToken> thisVar in (JObject)myBooks)
                        {
                            if (thisVar.Key == readingSession.GetBookTitle())
                            {
                                lastSessionInfo[0].Add((int)thisVar.Value["_visitedParagraphs"]?.Last);
                                lastSessionInfo[1] = (thisVar.Value?["_visitedParagraphs"]?.ToObject<List<int>>());
                            }
                        }
                    return lastSessionInfo;
                }
            }
            return null;
        }
    }

    public class SaveSessions
    {
        public readonly IDictionary<string, VisitedParagraphs> _booksSaved;

        public SaveSessions()
        {
            _booksSaved = new Dictionary<string, VisitedParagraphs>();
        }

        public void AddBookSession(string bookName, IList<int> visitedParagraphs) 
            => _booksSaved.Add(bookName, new VisitedParagraphs(visitedParagraphs));

    }

    public class VisitedParagraphs
    {
        public IList<int> _visitedParagraphs;

        public VisitedParagraphs(IList<int> visitedParagraphs)
        {
            _visitedParagraphs = new List<int>(visitedParagraphs);
        }
    }
}