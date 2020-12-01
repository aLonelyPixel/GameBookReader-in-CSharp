using System.Collections.Generic;

namespace GameBook.Domain
{
    public interface IBook
    {
        public string Name { get; }
        public string GetParagraphText(int paragraphIndex);
        public IDictionary<string, int> GetParagraphChoices(int paragraphIndex);
        public bool ParagraphIsFinal(int currentParagraph);
        public string GetParagraphLabel(int paragraphIndex);
    }
}