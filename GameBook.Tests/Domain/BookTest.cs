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
            var myBook = new Book("My Cool Book");

            Assert.AreEqual("My Cool Book", myBook.Name);
        }

        [Test]
        public void KnowsAmountOfParagraphs()
        {
            Choice choice1 = new Choice("choice1", 2);
            Choice choice2 = new Choice("choice2", 3);
            Paragraph paragraph1 = new Paragraph(1, "testing this", choice1);
            Paragraph paragraph2 = new Paragraph(2, "testing that", choice2);
            Paragraph paragraph3 = new Paragraph(3, "testing YAS");
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
        public void GetSpecificParagraphTestingBehaviour()
        {
            Mock<Paragraph> paragraphMock1 = new Mock<Paragraph>(1, "testing this");
            var myBook = new Book("My Cool Book", paragraphMock1.Object);
            var index = myBook.GetParagraph(0).Index;

            paragraphMock1.VerifyGet(paragraph => paragraph.Index, Times.AtLeastOnce);
        }

        [Test]
        public void GetSpecificParagraphTestingValues()
        {
            Choice choice1 = new Choice("choice1", 2);
            Choice choice2 = new Choice("choice2", 3);
            Paragraph paragraph1 = new Paragraph(1, "testing this", choice1);
            Paragraph paragraph2 = new Paragraph(2, "testing that", choice2);
            Paragraph paragraph3 = new Paragraph(3, "testing YAS");
            var myBook = new Book("My Cool Book", paragraph1, paragraph2, paragraph3);

            Assert.IsTrue(myBook.ContainsParagraph(3));
            Assert.AreEqual(3, myBook.GetParagraph(3).Index);
        }

        [Test]
        public void GetParagraphTextTestingBehaviour()
        {
            Mock<Paragraph> paragraphMock1 = new Mock<Paragraph>(1, "testing this");
            var myBook = new Book("My Cool Book", paragraphMock1.Object);
            
            myBook.GetParagraphText(0);
            paragraphMock1.VerifyGet(paragraph => paragraph.Text, Times.Once);
        }

        [Test]
        public void GetParagraphTextTestingValues()
        {
            Choice choiceMock1 = new Choice("choice1", 2);
            Choice choiceMock2 = new Choice("choice2", 3);
            Paragraph paragraphMock1 = new Paragraph(1, "testing this", choiceMock1);
            Paragraph paragraphMock2 = new Paragraph(2, "testing that", choiceMock2);
            Paragraph paragraphMock3 = new Paragraph(3, "testing YAS");
            var myBook = new Book("My Cool Book", paragraphMock1, paragraphMock2, paragraphMock3);

            Assert.AreEqual("testing that", myBook.GetParagraphText(2));
        }

        [Test]
        public void GetParagraphChoicesTestingBehaviour()
        {
            Mock<Choice> choiceMock1 = new Mock<Choice>("choice1", 2);
            Mock<Choice> choiceMock2 = new Mock<Choice>("choice2", 3);
            Mock<Paragraph> paragraphMock1 = new Mock<Paragraph>(1, "testing this", choiceMock1.Object, choiceMock2.Object);
            paragraphMock1.Setup(paragraph => paragraph.Index).Returns(1);
            var myBook = new Book("My Cool Book", paragraphMock1.Object);
            myBook.GetParagraphChoices(1);
            
            paragraphMock1.Verify(paragraph => paragraph.GetChoices(), Times.Once);
        }

        [Test]
        public void GetParagraphChoicesTestingValues()
        {
            Choice choiceMock1 = new Choice("choice1", 2);
            Choice choiceMock2 = new Choice("choice2", 3);
            Paragraph paragraphMock1 = new Paragraph(1, "testing this", choiceMock1, choiceMock2);
            Paragraph paragraphMock2 = new Paragraph(2, "testing that", choiceMock2);
            var myBook = new Book("My Cool Book", paragraphMock1, paragraphMock2);
            IList<string> choicesList = new List<string>
            {
                "choice1",
                "choice2"
            };

            Assert.AreEqual(choicesList, myBook.GetParagraphChoices(1));
        }

/*        [Test]
        public void GetChoiceDestinationTestingBehaviour()
        {
            Mock<Choice> choiceMock1 = new Mock<Choice>("choice1", 2);
            Mock<Choice> choiceMock2 = new Mock<Choice>("choice2", 3);
            Mock<Paragraph> paragraphMock1 = new Mock<Paragraph>(1, "testing this", choiceMock1.Object, choiceMock2.Object, MockBehavior.Loose);
            var myBook = new Book("My Cool Book", paragraphMock1.Object);
            myBook.GetChoiceDestination(0, 0);

            paragraphMock1.Verify(paragraph => paragraph.GetChoiceDestination(0, 0), Times.Once);
        }*/

        [Test]
        public void GetChoiceDestinationTestingValues()
        {
            Choice choiceMock1 = new Choice("choice1", 2);
            Choice choiceMock2 = new Choice("choice2", 3);
            Paragraph paragraphMock1 = new Paragraph(1, "testing this", choiceMock1, choiceMock2);
            Paragraph paragraphMock2 = new Paragraph(2, "testing that", choiceMock2);
            var myBook = new Book("My Cool Book", paragraphMock1, paragraphMock2);

            /*Assert.AreEqual(3, myBook.GetChoiceDestination(1, 1));
            Assert.AreEqual(2, myBook.GetChoiceDestination(1, 0));
            Assert.AreEqual(3, myBook.GetChoiceDestination(2, 0));*/
        }

        [Test]
        public void ChecksIfParagraphIsFinalTestingBehaviour()
        {
            Mock<Choice> choiceMock1 = new Mock<Choice>("choice1", 2);
            Mock<Choice> choiceMock2 = new Mock<Choice>("choice2", 3);
            Mock<Paragraph> paragraphMock1 = new Mock<Paragraph>(1, "testing this", choiceMock1.Object, choiceMock2.Object);
            Mock<Paragraph> paragraphMock2 = new Mock<Paragraph>(1, "testing that");
            paragraphMock1.Setup(paragraph => paragraph.IsTerminal()).Returns(false);
            paragraphMock1.Setup(paragraph => paragraph.Index).Returns(1);
            paragraphMock2.Setup(paragraph => paragraph.IsTerminal()).Returns(true);
            paragraphMock2.Setup(paragraph => paragraph.Index).Returns(1);
            var myBook = new Book("My Cool Book", paragraphMock1.Object);
            var myBook2 = new Book("My Cool Book", paragraphMock2.Object);
            myBook.ParagraphIsFinal(1);
            myBook2.ParagraphIsFinal(1);

            paragraphMock1.Verify(paragraph => paragraph.IsTerminal(), Times.Once);
            paragraphMock2.Verify(paragraph => paragraph.IsTerminal(), Times.Once);
        }

        [Test]
        public void ChecksIfParagraphIsFinalTestingValues()
        {
            Choice choiceMock1 = new Choice("choice1", 2);
            Choice choiceMock2 = new Choice("choice2", 3);
            Paragraph paragraphMock1 = new Paragraph(1, "testing this", choiceMock1, choiceMock2);
            Paragraph paragraphMock2 = new Paragraph(2, "testing that");
            var myBook = new Book("My Cool Book", paragraphMock1, paragraphMock2);

            Assert.IsFalse(myBook.ParagraphIsFinal(1));
            Assert.IsTrue(myBook.ParagraphIsFinal(2));
        }

        [Test]
        public void GetsParagraphsLabelsTestingBehaviour()
        {
            Mock<Paragraph> paragraphMock1 = new Mock<Paragraph>(1, "testing this");
            var myBook = new Book("My Cool Book", paragraphMock1.Object);
            IList<int> visitedParagraphs = new List<int>();
            visitedParagraphs.Add(0);
            //myBook.GetParagraphsLabels(visitedParagraphs);

            paragraphMock1.Verify(paragraph => paragraph.GetLabel(), Times.Once);
        }

        [Test]
        public void GetsParagraphsLabelsTestingValues()
        {
            Choice choice1 = new Choice("choice1", 2);
            Choice choice2 = new Choice("choice2", 3);
            Paragraph paragraph1 = new Paragraph(1, "testing this", choice1, choice2);
            Paragraph paragraph2 = new Paragraph(2, "testing that");
            var myBook = new Book("My Cool Book", paragraph1, paragraph2);
            IList<string> paragraphLabels = new List<string>();
            paragraphLabels.Add("testing this ...");
            paragraphLabels.Add("testing that ...");
            IList<int> visitedParagraphs = new List<int>();
            visitedParagraphs.Add(1);
            visitedParagraphs.Add(2);

            //Assert.AreEqual(paragraphLabels, myBook.GetParagraphsLabels(visitedParagraphs));
        }

        [Test]
        public void GetsParagraphIndex()
        {
            Mock<Paragraph> paragraphMock1 = new Mock<Paragraph>(1, "testing this");
            var myBook = new Book("My Cool Book", paragraphMock1.Object);

            paragraphMock1.VerifyGet(paragraph => paragraph.Index, Times.Once);
        }

        [Test]
        public void GetsParagraphIndexWhenAbsent()
        {
            Paragraph paragraphMock1 = new Paragraph(1, "testing this");
            var myBook = new Book("My Cool Book", paragraphMock1);

            //Assert.AreEqual(-1, myBook.GetParagraphIndex("un paragraphe bidon ..."));
        }
    }
}