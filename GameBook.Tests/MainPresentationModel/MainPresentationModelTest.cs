using GameBook.Domain;
using Moq;
using NUnit.Framework;

namespace GameBook.Tests.MainPresentationModel
{
    [TestFixture]
    public class MainPresentationModelTest
    {
        [Test]
        public void Test1()
        {
            Mock<Book> bookMock = new Mock<Book>("fake title");
            Mock<ReadingSession> readingMock = new Mock<ReadingSession>(bookMock.Object);
            ViewModel.ViewModel mpModel = new ViewModel.ViewModel(readingMock.Object);
            //mpModel.GetBookTitle();

            readingMock.Verify(readingSession => readingSession.GetBookTitle(), Times.Once);
        }
    }
}