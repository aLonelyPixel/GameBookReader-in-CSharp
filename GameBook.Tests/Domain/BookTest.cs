using System.Collections.Generic;
using GameBook.Domain;
using Moq;
using NUnit.Framework;

namespace GameBook.Tests.Domain
{
    [TestFixture]
    public class BookTest
    {
        [Test]
        public void KnowsItsName()
        {
            var p1 = new Paragraph(1, "testingThis", new List<Choice>());
            var p2 = new Paragraph(2, "testingThis", new List<Choice>());
            var myBook = new Book("My Cool Book", p1, p2);
            Assert.AreEqual("My Cool Book", myBook.Name);

            var p3 = new Paragraph(1, "testingThis", new List<Choice>());
            var p4 = new Paragraph(2, "testingThis", new List<Choice>());
            var pList = new List<Paragraph> {p3, p4};
            var myBook2 = new Book("Another cool book", pList);

            Assert.AreEqual("Another cool book", myBook2.Name);
        }

        [Test]
        public void KnowsParagraphsText()
        {
            var choice1 = new Choice("choice1", 2);
            var choice2 = new Choice("choice2", 3);
            var choiceList = new List<Choice> {choice1, choice2};
            var paragraph1 = new Paragraph(1, "testing this", choiceList);
            var paragraph2 = new Paragraph(2, "testing that", choiceList);
            var paragraph3 = new Paragraph(3, "testing YAS", new List<Choice>());
            var myBook = new Book("My Cool Book", paragraph1, paragraph2, paragraph3);

            Assert.AreEqual("testing this", myBook.GetParagraphText(1));
            Assert.AreEqual("testing that", myBook.GetParagraphText(2));
            Assert.AreEqual("testing YAS", myBook.GetParagraphText(3));
        }

        [Test]
        public void RecoversChoicesFromParagraph()
        {
            var choice1 = new Choice("choice1", 2);
            var choice2 = new Choice("choice2", 3);
            var choiceList = new List<Choice> { choice1, choice2 };
            var paragraph1 = new Paragraph(1, "testing this", choiceList);
            var paragraph2 = new Paragraph(3, "testing YAS", new List<Choice>());
            var myBook = new Book("My Cool Book", paragraph1, paragraph2);
            IDictionary<string, int> choices = new Dictionary<string, int>();
            choices.Add("choice1", 2);
            choices.Add("choice2", 3);
            Assert.AreEqual(choices, myBook.GetParagraphChoices(1));

            choices.Clear();
            Assert.AreEqual(choices, myBook.GetParagraphChoices(2));
        }

        [Test]
        public void KnowsParagraphIsFinal()
        {
            var paragraph3 = new Paragraph(3, "testing YAS", new List<Choice>());
            var myBook = new Book("My Cool Book", paragraph3);
            Assert.IsTrue(myBook.ParagraphIsFinal(3));
        }

        [Test]
        public void GetParagraphLabel()
        {
            var paragraph3 = new Paragraph(3, "testing YAS", new List<Choice>());
            var myBook = new Book("My Cool Book", paragraph3);
            Assert.AreEqual("testing YAS ...", myBook.GetParagraphLabel(3));
        }
    }
}