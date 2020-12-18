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
            var mainObject = (JObject) JToken.ReadFrom(jtr);
            var bookTitle = mainObject["Title"]?.ToString();
            var paragraphs = (JArray)mainObject["Paragraphs"];
            if (paragraphs == null) return new Book(bookTitle, paragraphList);
            ExtractParagraphs(paragraphs, paragraphList);
            return new Book(bookTitle, paragraphList);
        }

        private static void ExtractParagraphs(JArray paragraphs, ICollection<Paragraph> paragraphList)
        {
            foreach (var paragraph in paragraphs)
            {
                IList<Choice> choiceList = new List<Choice>();
                JArray choices = (JArray) paragraph["Choices"];
                if (choices != null) ExtractChoices(choices, choiceList);

                paragraphList.Add(new Paragraph((int) paragraph["Index"], (string) paragraph["Text"], choiceList));
            }
        }

        private static void ExtractChoices(JArray choices, ICollection<Choice> choiceList)
        {
            foreach (var choice in choices)
            {
                choiceList.Add(
                    new Choice((string) choice["Choice text"], (int) choice["Destination paragraph"]));
            }
        }
    }
}