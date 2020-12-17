using System.Collections.Generic;
using System.IO;
using GameBook.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GameBook.io
{
    public class JsonLoader : IBookLoader
    {
        public IBook LoadBook(string path)
        {
            IList<Paragraph> paragraphList = new List<Paragraph>();

            using FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read);
            using JsonTextReader jtr = new JsonTextReader(new StreamReader(fs));

            JObject mainObject = (JObject) JToken.ReadFrom(jtr);
            var bookTitle = mainObject["Title"]?.ToString();
            JArray paragraphs = (JArray)mainObject["Paragraphs"];
            foreach (var paragraph in (JArray)paragraphs)
            {
                IList<Choice> choiceList = new List<Choice>();
                JArray choices = (JArray) paragraph["Choices"];
                foreach (var choice in choices)
                {
                    choiceList.Add(new Choice((string)choice["Choice text"], (int)choice["Destination paragraph"]));
                }
                paragraphList.Add(new Paragraph((int)paragraph["Index"], (string)paragraph["Text"], choiceList));
            }

            return new Book(bookTitle, paragraphList);
        }
    }
}