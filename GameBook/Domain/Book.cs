using System.Collections.Generic;

namespace GameBook.Domain
{
    public class Book
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
    }
}
