using GameBook.Domain;
using NUnit.Framework;

namespace GameBook.Tests.Domain
{
    [TestFixture]
    public class MainPresentationModelTest
    {
        [Test]
        public void KnowsBookTitle()
        {
            var myBook = new Book("My Cool Book");
            var mpModel = new MainPresentationModel(myBook);

            Assert.AreEqual("My Cool Book", mpModel.GetBookTitle());
        }

        [Test]
        public void GetsCorrectParagraph()
        {
            var p3 = new Paragraph(3, "Jim est finalment hydraté");
            var myBook = new Book("My Cool Book", p3);
            var mpModel = new MainPresentationModel(myBook);

            Assert.AreEqual(p3, mpModel.GetParagraph(3));
        }

        [Test]
        public void ReturnsErrorIfNoParagraphs()
        {
            var myBook = new Book("My Cool Book");
            var mpModel = new MainPresentationModel(myBook);

            Assert.AreEqual(null, mpModel.GetParagraph(3));
        }

        [Test]
        public void KnowsBeforeLastParagraph()
        {
            var myBook = new Book("My Cool Book");
            var mpModel = new MainPresentationModel(myBook);
            mpModel.AddReadParagraph(1);
            mpModel.AddReadParagraph(3);
            mpModel.AddReadParagraph(4);

            Assert.AreEqual(1, mpModel.GetPreviousParagraph());
        }

        [Test]
        public void ReturnsErrorOnFirstParagraph()
        {
            var myBook = new Book("My Cool Book");
            var mpModel = new MainPresentationModel(myBook);
            mpModel.AddReadParagraph(1);

            Assert.AreEqual(1, mpModel.GetPreviousParagraph());
        }

        [Test]
        public void ReturnsErrorOnNothingRead()
        {
            var myBook = new Book("My Cool Book");
            var mpModel = new MainPresentationModel(myBook);

            Assert.AreEqual(-1, mpModel.GetPreviousParagraph());
        }

        [Test]
        public void HistoryIsEmptyUponReset()
        {
            var myBook = new Book("My Cool Book");
            var mpModel = new MainPresentationModel(myBook);
            mpModel.AddReadParagraph(1);
            mpModel.AddReadParagraph(3);
            mpModel.AddReadParagraph(4);
            mpModel.ClearHistory();

            Assert.AreEqual(-1, mpModel.GetPreviousParagraph());
        }
    }
}