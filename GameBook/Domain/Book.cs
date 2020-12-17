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

        public Book(string bookName, IList<Paragraph> newParagraphs)
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
        
        public Paragraph GetParagraph(int paragraphIndex) 
            => ContainsParagraph(paragraphIndex) ? Paragraphs[paragraphIndex] : new Paragraph(0, "");

        public string GetParagraphText(int paragraphIndex)
            => ContainsParagraph(paragraphIndex) ? Paragraphs[paragraphIndex].Text : null;

        public IDictionary<string, int> GetParagraphChoices(int paragraphIndex) 
            => ContainsParagraph(paragraphIndex) ? Paragraphs[paragraphIndex].GetChoices() : new Dictionary<string, int>();

        public bool ParagraphIsFinal(int currentParagraph) 
            => ContainsParagraph(currentParagraph) && Paragraphs[currentParagraph].IsTerminal();

        public string GetParagraphLabel(int paragraphIndex)
            => ContainsParagraph(paragraphIndex) ? Paragraphs[paragraphIndex].GetLabel() : "";
    }
}