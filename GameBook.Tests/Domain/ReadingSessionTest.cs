/*using System.Collections.Generic;
using System.Linq;
using GameBook.Domain;
using Moq;
using NUnit.Framework;

namespace GameBook.Tests.Domain
{
    [TestFixture]
    public class ReadingSessionTest
    {
        [Test]
        public void InitialisedValues()
        {
            Mock<Book> bookMock = new Mock<Book>("A very cool book");
            ReadingSession readingSession = new ReadingSession(bookMock.Object);

            Assert.AreEqual(1, readingSession.GetCurrentParagraph());
            Assert.AreEqual("A very cool book", readingSession.GetBookTitle());
        }

        [Test]
        public void GetParagraphTextTestingBehaviour()
        {
            Mock<Paragraph> paragraphMock1 = new Mock<Paragraph>(1, "this is the first paragraph");
            Mock<Book> bookMock = new Mock<Book>("A very cool book", paragraphMock1.Object);
            ReadingSession readingSession = new ReadingSession(bookMock.Object);
            //readingSession.GetParagraphText(0);

            bookMock.Verify(book => book.GetParagraphText(0), Times.Once);
        }

        [Test]
        public void GetParagraphTextTestingValues()
        {
            Paragraph paragraph1 = new Paragraph(1, "this is the first paragraph");
            Paragraph paragraph2 = new Paragraph(2, "this is the second paragraph");
            Book book = new Book("A very cool book", paragraph1, paragraph2);
            ReadingSession readingSession = new ReadingSession(book);
            //readingSession.GetParagraphText(1);

            //Assert.AreEqual("this is the first paragraph", readingSession.GetParagraphText(1));
        }

        [Test]
        public void GetParagraphChoicesTestingBehaviour()
        {
            Mock<Paragraph> paragraphMock1 = new Mock<Paragraph>(1, "this is the first paragraph");
            Mock<Book> bookMock = new Mock<Book>("A very cool book", paragraphMock1.Object);
            ReadingSession readingSession = new ReadingSession(bookMock.Object);
            readingSession.GetParagraphChoices(0);

            bookMock.Verify(book => book.GetParagraphChoices(0), Times.Once);
        }

        [Test]
        public void GoToParagraphByChoice()
        {
            Choice choice1 = new Choice("icantanymorewiththiscovidhelpme", 2);
            Choice choice2 = new Choice("plsendthis", 1);
            Paragraph paragraph1 = new Paragraph(1, "this is the first paragraph", choice1);
            Paragraph paragraph2 = new Paragraph(2, "this is the second paragraph", choice2);
            Book book = new Book("A very cool book", paragraph1, paragraph2);
            ReadingSession readingSession = new ReadingSession(book);
            readingSession.GoToParagraphByChoice(0);

            Assert.AreEqual(2, readingSession.GetCurrentParagraph());

            readingSession.GoToParagraphByChoice(0);

            Assert.AreEqual(1, readingSession.GetCurrentParagraph());
        }

        [Test]
        public void GetVisitedParagraphs()
        {
            Choice choice1 = new Choice("icantanymorewiththiscovidhelpme", 2);
            Choice choice2 = new Choice("plsendthis", 1);
            Paragraph paragraph1 = new Paragraph(1, "this is the first paragraph", choice1);
            Paragraph paragraph2 = new Paragraph(2, "this is the second paragraph", choice2);
            Book book = new Book("A very cool book", paragraph1, paragraph2);
            ReadingSession readingSession = new ReadingSession(book);
            readingSession.GoToParagraphByChoice(0);
            readingSession.GoToParagraphByChoice(0);
            //IList<string> list = (IList<string>)readingSession.GetVisitedParagraphs();

            //Assert.AreEqual(3, readingSession.GetVisitedParagraphs().Count());
            //Assert.AreEqual("this is the first ...", list[0]);
            //Assert.AreEqual("this is the second ...", list[1]);
        }

        [Test]
        public void CheckIfStoryHasEnded()
        {
            Mock<Book> bookMock = new Mock<Book>("A very cool book");
            ReadingSession readingSession = new ReadingSession(bookMock.Object);
            //readingSession.HasStoryEnded();

            bookMock.Verify(book => book.ParagraphIsFinal(1), Times.Once);
        }

        [Test]
        public void GoBackToPreviousParagraph()
        {
            Choice choice1 = new Choice("icantanymorewiththiscovidhelpme", 2);
            Choice choice2 = new Choice("plsendthis", 1);
            Paragraph paragraph1 = new Paragraph(1, "this is the first paragraph", choice1);
            Paragraph paragraph2 = new Paragraph(2, "this is the second paragraph", choice2);
            Book book = new Book("A very cool book", paragraph1, paragraph2);
            ReadingSession readingSession = new ReadingSession(book);
            readingSession.GoToParagraphByChoice(0);
            readingSession.GoToParagraphByChoice(0);
            readingSession.GoBackToPrevious();

            Assert.AreEqual(2, readingSession.GetCurrentParagraph());
        }

        [Test]
        public void GoBackToPreviousParagraphButYouJustStarted() //You can't go back if you just started reading
        {
            Choice choice1 = new Choice("icantanymorewiththiscovidhelpme", 2);
            Choice choice2 = new Choice("plsendthis", 1);
            Paragraph paragraph1 = new Paragraph(1, "this is the first paragraph", choice1);
            Paragraph paragraph2 = new Paragraph(2, "this is the second paragraph", choice2);
            Book book = new Book("A very cool book", paragraph1, paragraph2);
            ReadingSession readingSession = new ReadingSession(book);
            readingSession.GoBackToPrevious();

            Assert.AreEqual(1, readingSession.GetCurrentParagraph());
        }

        [Test]
        public void GoToAVisitedParagraph()
        {
            Choice choice1 = new Choice("icantanymorewiththiscovidhelpme", 2);
            Choice choice2 = new Choice("plsendthis", 1);
            Paragraph paragraph1 = new Paragraph(1, "this is the first paragraph", choice1);
            Paragraph paragraph2 = new Paragraph(2, "this is the second paragraph", choice2);
            Book book = new Book("A very cool book", paragraph1, paragraph2);
            ReadingSession readingSession = new ReadingSession(book);
            readingSession.GoToParagraphByChoice(0);
            //readingSession.GoToVisitedParagraph("this is the first ...");

            Assert.AreEqual(1, readingSession.GetCurrentParagraph());
        }

        [Test]
        public void AdjustsVisitedParagraphsWhenGoingBack()
        {
            Choice choice1 = new Choice("icantanymorewiththiscovidhelpme", 2);
            Choice choice2 = new Choice("plsendthis", 1);
            Paragraph paragraph1 = new Paragraph(1, "this is the first paragraph", choice1);
            Paragraph paragraph2 = new Paragraph(2, "this is the second paragraph", choice2);
            Book book = new Book("A very cool book", paragraph1, paragraph2);
            ReadingSession readingSession = new ReadingSession(book);
            readingSession.GoToParagraphByChoice(0);
            //readingSession.GoToVisitedParagraph("this is the first ...");

            //Assert.AreEqual(1, readingSession.GetVisitedParagraphs().Count());
        }

        [Test]
        public void AdjustsVisitedParagraphsWhenGoingBackButYouJustStarted()
        {
            Choice choice1 = new Choice("icantanymorewiththiscovidhelpme", 2);
            Choice choice2 = new Choice("plsendthis", 1);
            Paragraph paragraph1 = new Paragraph(1, "this is the first paragraph", choice1);
            Paragraph paragraph2 = new Paragraph(2, "this is the second paragraph", choice2);
            Book book = new Book("A very cool book", paragraph1, paragraph2);
            ReadingSession readingSession = new ReadingSession(book);
            readingSession.GoToParagraphByChoice(0);
            readingSession.GoBackToPrevious();
            //readingSession.GoToVisitedParagraph("this is the first ...");

            //Assert.AreEqual(1, readingSession.GetVisitedParagraphs().Count());
        }
    }
}*/