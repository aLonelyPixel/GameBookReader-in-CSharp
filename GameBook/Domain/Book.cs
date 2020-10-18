using System;
using System.Collections.Generic;

namespace GameBook.Domain
{
    public class Book
    {
        private Dictionary<int, Paragraph> _paragraphs = new Dictionary<int, Paragraph>();
        public Book(string bookName, params Paragraph[] newParagraphs)
        {
            Name = bookName;
            foreach (var paragraphs in newParagraphs)
            {
                _paragraphs.Add(paragraphs.Index, paragraphs);
            }
        }
        public string Name { get; }

        public Dictionary<int, Paragraph> Paragraphs
        {
            get { return _paragraphs; }
        }

        public bool ContainsParagraph(int paragraphIndex) => _paragraphs.ContainsKey(paragraphIndex);
        
        public Paragraph GetParagraph(int paragraphIndex) => _paragraphs[paragraphIndex];
        
    }
}
