namespace GameBook.Domain
{
    public class Choice
    {
        public Choice(string text, int destParagraph)
        {
            Text = text;
            DestParagraph = destParagraph;
        }
        public string Text { get; }

        public int DestParagraph { get; }
    }
}