using System.Collections.Generic;
using GameBook.Domain;
using Moq;
using NUnit.Framework;

namespace GameBook.Tests.Domain
{
    [TestFixture]
    public class ParagraphTest
    {
        [Test]
        public void KnowsItsText()
        {
            Mock<Choice> choicheMock1 = new Mock<Choice>("choice1", 2);
            Mock<Choice> choicheMock2 = new Mock<Choice>("choice2", 3); 
            Mock<Choice> choicheMock3 = new Mock<Choice>("choice3", 9);
            var paragraph1 = new Paragraph(1, "testing this", choicheMock1.Object, choicheMock2.Object, choicheMock3.Object);
            Assert.AreEqual("testing this", paragraph1.Text);
        }

        [Test]
        public void KnowsAmountOfChoices()
        {
            Mock<Choice> choicheMock1 = new Mock<Choice>("choice1", 2);
            Mock<Choice> choicheMock2 = new Mock<Choice>("choice2", 3);
            Mock<Choice> choicheMock3 = new Mock<Choice>("choice3", 9);
            var paragraph1 = new Paragraph(1, "testing this", choicheMock1.Object, choicheMock2.Object, choicheMock3.Object);
            Assert.AreEqual(3, paragraph1.Choices.Count);
            Assert.IsFalse(paragraph1.IsTerminal());
        }

        [Test]
        public void KnowsItsTerminal()
        {
            var paragraph1 = new Paragraph(1, "testing this");
            Assert.IsTrue(paragraph1.IsTerminal());
        }

        [Test]
        public void GetsChoices()
        {
            Mock<Choice> choicheMock1 = new Mock<Choice>("choice1", 2);
            Mock<Choice> choicheMock2 = new Mock<Choice>("choice2", 3);
            Mock<Choice> choicheMock3 = new Mock<Choice>("choice3", 9);
            var paragraph1 = new Paragraph(1, "testing this", choicheMock1.Object, choicheMock2.Object, choicheMock3.Object);
            IList<string> choicesList = new List<string>();
            choicesList.Add("choice1");
            choicesList.Add("choice2");
            choicesList.Add("choice3");
            
            Assert.AreEqual(choicesList, paragraph1.GetChoices());
        }

        [Test]
        public void GetsChoicesDestination()
        {
            Mock<Choice> choicheMock1 = new Mock<Choice>("choice1", 2);
            Mock<Choice> choicheMock2 = new Mock<Choice>("choice2", 3);
            Mock<Choice> choicheMock3 = new Mock<Choice>("choice3", 9);
            var paragraph1 = new Paragraph(1, "testing this", choicheMock1.Object, choicheMock2.Object, choicheMock3.Object);
            Assert.AreEqual(2, paragraph1.GetChoiceDestination(0));
            Assert.AreEqual(3, paragraph1.GetChoiceDestination(1));
            Assert.AreEqual(9, paragraph1.GetChoiceDestination(2));
            Assert.AreEqual(-1, paragraph1.GetChoiceDestination(-3));
        }

        [Test]
        public void GetParagraphLabel()
        {
            var paragraph1 = new Paragraph(1, "testing this");
            Assert.AreEqual("testing this ...", paragraph1.GetLabel());
        }
    }
}