namespace GameBook
{
    public class Choice
    {
        public Choice(string text, int destParagraph)
        {
            this.Text = text;
            this.DestParagraph = destParagraph;
        }
        public string Text { get; }

        public int DestParagraph { get; }
    }
}