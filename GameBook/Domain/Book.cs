using System.Collections.Generic;

namespace GameBook.Domain
{
    public class Book : IBook
    {
        public Book(string bookName, params Paragraph[] newParagraphs)
        {
            Name = bookName;
            foreach (var paragraphs in newParagraphs)
            {
                Paragraphs.Add(paragraphs.Index, paragraphs);
            }
        }

        public string Name { get; }
        public Dictionary<int, Paragraph> Paragraphs { get; } = new Dictionary<int, Paragraph>();

        public bool ContainsParagraph(int paragraphIndex) => Paragraphs.ContainsKey(paragraphIndex);
        
        public Paragraph GetParagraph(int paragraphIndex) => Paragraphs[paragraphIndex];

        public string GetParagraphText(int paragraphIndex) => Paragraphs[paragraphIndex].Text;

        public IDictionary<string, int> GetParagraphChoices(int paragraphIndex) => 
            Paragraphs[paragraphIndex].GetChoices();

        public bool ParagraphIsFinal(int currentParagraph) => Paragraphs[currentParagraph].IsTerminal();

        public string GetParagraphLabel(int paragraphIndex) => Paragraphs[paragraphIndex].GetLabel();
    }
}