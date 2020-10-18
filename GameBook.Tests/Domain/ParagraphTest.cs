using GameBook.Domain;
using NUnit.Framework;

namespace GameBook.Tests.Domain
{
    [TestFixture]
    public class ParagraphTest
    {
        [Test]
        public void KnowsItsText()
        {
            var testChoice1 = new Choice("choice1", 2);
            var testChoice2 = new Choice("choice2", 3);
            var testChoice3 = new Choice("choice3", 9);
            var paragraph1 = new Paragraph(1, "testing this", testChoice1, testChoice2, testChoice3);
            Assert.AreEqual("testing this", paragraph1.Text);
        }

        [Test]
        public void KnowsAmountOfChoices()
        {
            var testChoice1 = new Choice("choice1", 2);
            var testChoice2 = new Choice("choice2", 3);
            var testChoice3 = new Choice("choice3", 9);
            var paragraph1 = new Paragraph(1, "testing this", testChoice1, testChoice2, testChoice3);
            Assert.AreEqual(3, paragraph1.Choices.Count);
            Assert.IsFalse(paragraph1.IsTerminal());
        }

        [Test]
        public void KnowsItsTerminal()
        {
            var paragraph1 = new Paragraph(1, "testing this");
            Assert.IsTrue(paragraph1.IsTerminal());
        }
    }
}