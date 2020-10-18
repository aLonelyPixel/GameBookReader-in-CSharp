using GameBook.Domain;
using NUnit.Framework;

namespace GameBook.Tests.Domain
{
    [TestFixture]
    public class BookTest
    {
        [Test]
        public void KnowsItsName()
        {
            var myBook = new Book("My Cool Book");
            Assert.AreEqual("My Cool Book", myBook.Name);
        }

        [Test]
        public void KnowsAmountOfParagraphs()
        {
            var testChoice1 = new Choice("choice1", 2);
            var testChoice2 = new Choice("choice2", 3);
            var paragraph1 = new Paragraph(1, "testing this", testChoice1);
            var paragraph2 = new Paragraph(2, "testing that", testChoice2);
            var paragraph3 = new Paragraph(3, "testing YAS"); 
            var myBook = new Book("My Cool Book", paragraph1, paragraph2, paragraph3);
            Assert.AreEqual("My Cool Book", myBook.Name);
            Assert.AreEqual(3, myBook.Paragraphs.Count);
        }

        [Test]
        public void KnowsItsEmpty()
        {
            var myBook = new Book("My Cool Book");
            Assert.AreEqual(0, myBook.Paragraphs.Count);
        }

        [Test]
        public void GetSpecificParagraph()
        {
            var testChoice1 = new Choice("choice1", 2);
            var testChoice2 = new Choice("choice2", 3);
            var paragraph1 = new Paragraph(1,"testing this", testChoice1);
            var paragraph2 = new Paragraph(2,"testing that", testChoice2);
            var paragraph3 = new Paragraph(3,"testing YAS");
            var myBook = new Book("My Cool Book", paragraph1, paragraph2, paragraph3);
            Assert.AreEqual(3, myBook.GetParagraph(3).Index);
        }
    }
}