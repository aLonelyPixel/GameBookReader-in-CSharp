using GameBook.Domain;
using NUnit.Framework;

namespace GameBook.Tests.Domain
{
    public class ChoiceTest
    {
        [Test]
        public void KnowsItsText()
        {
            Choice testChoice = new Choice("choice1", 2);

            Assert.AreEqual("choice1", testChoice.Text);
        }

        [Test]
        public void KnowsItsDestParagraph()
        {
            Choice testChoice = new Choice("choice1", 2);

            Assert.AreEqual(2, testChoice.DestParagraph);
        }
    }
}