using System.Collections.Generic;
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
            var rs = new ReadingSession();

            Assert.AreEqual(1, rs.GetCurrentParagraph());

            var bookMock = new Mock<IBook>();
            bookMock.Setup(book => book.Name).Returns("Cool book");
            rs.SetBook(bookMock.Object, "");

            Assert.AreEqual("Cool book", rs.GetBookTitle());
            Assert.AreEqual("", rs.Path);
            Assert.AreEqual("No message", rs.WarningMessage);
        }
        
        [Test]
        public void GetBookTitleBehaviour()
        {
            var rs = new ReadingSession();
            var bookMock = new Mock<IBook>();
            rs.SetBook(bookMock.Object, "");
            rs.GetBookTitle();

            bookMock.Verify(book => book.Name, Times.Once);
        }

        [Test]
        public void GetParagraphTextBehaviour()
        {
            var rs = new ReadingSession();
            var bookMock = new Mock<IBook>();
            rs.SetBook(bookMock.Object, "");
            rs.GetParagraphContent();

            bookMock.Verify(book => book.GetParagraphText(1), Times.Once);
        }

        [Test]
        public void GetParagraphChoicesBehaviour()
        {
            var rs = new ReadingSession();
            var bookMock = new Mock<IBook>();
            rs.SetBook(bookMock.Object, "");
            rs.GetParagraphChoices(1);

            bookMock.Verify(book => book.GetParagraphChoices(1), Times.Once);
        }

        [Test]
        public void GoToParagraphByChoice()
        {
            var rs = new ReadingSession();
            var bookMock = new Mock<IBook>();
            rs.SetBook(bookMock.Object, "");
            rs.GoToParagraphByChoice(2);

            Assert.AreEqual(2, rs.GetCurrentParagraph());
            bookMock.Verify(book => book.ParagraphIsFinal(2), Times.Once);
        }

        [Test]
        public void GetReadingHistory()
        {
            var rs = new ReadingSession();
            var bookMock = new Mock<IBook>();
            rs.SetBook(bookMock.Object, "");
            rs.GoToParagraphByChoice(2);
            rs.GoToParagraphByChoice(3);

            Assert.AreEqual(3, rs.GetHistory().Count);
            Assert.AreEqual(3, rs.GetVisitedParagraphs().Count);
        }

        [Test]
        public void ChecksIfFakeBook()
        {
            var rs = new ReadingSession();
            var bookMock = new Mock<IBook>();

            Assert.IsTrue(rs.IsFakeBook());
        }

        [Test]
        public void ResetsAfterOpeningSession()
        {
            var rs = new ReadingSession();
            var bookMock = new Mock<IBook>();
            rs.SetBook(bookMock.Object, "");
            IList<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            rs.OpenLastSession(list);

            Assert.AreEqual(3, rs.GetCurrentParagraph());
            Assert.AreEqual(3, rs.GetVisitedParagraphs().Count);
        }

        [Test]
        public void GoesBackToPreviousParagraph()
        {
            var rs = new ReadingSession();
            var bookMock = new Mock<IBook>();
            rs.SetBook(bookMock.Object, "");
            IList<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            rs.OpenLastSession(list);
            rs.GoBackToPrevious();
            Assert.AreEqual(2, rs.GetCurrentParagraph());
            Assert.AreEqual(2, rs.GetVisitedParagraphs().Count);
        }

        [Test]
        public void GoesBackToVisitedParagraph()
        {
            var rs = new ReadingSession();
            var bookMock = new Mock<IBook>();
            rs.SetBook(bookMock.Object, "");
            IList<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            rs.OpenLastSession(list);
            rs.GoToVisitedParagraph(1);

            Assert.AreEqual(1, rs.GetCurrentParagraph());
            Assert.AreEqual(1, rs.GetVisitedParagraphs().Count);
        }
    }
}