using System;
using System.Collections.Generic;
using GameBook.Domain;
using GameBook.io;
using GameBook.Wpf.ViewModels;
using GameBook.Wpf.ViewModels.Command;
using Moq;
using NUnit.Framework;

namespace GameBook.Tests.ViewModel
{
    [TestFixture]
    public class GameBookViewModelTest
    {
        [Test]
        public void CheckBookName()
        {
            var rs = new Mock<IReadingSession>();
            var fileRes = new Mock<IChooseResource>();
            var repos = new Mock<IReadingSessionRepository>();
            rs.Setup(readsess => readsess.GetBookTitle()).Returns("TestBook");
            rs.Setup(readsess => readsess.GetCurrentParagraph()).Returns(1);
            IDictionary<string, int> choices = new Dictionary<string, int>();
            choices.Add("choice1", 2);
            choices.Add("choice2", 3);
            rs.Setup(readsess => readsess.GetParagraphChoices(1)).Returns(choices);
            IDictionary<int, string> history = new Dictionary<int, string>();
            history.Add(1, "p1");
            history.Add(2, "p2");
            rs.Setup(readsess => readsess.GetHistory()).Returns(history);
            repos.Setup(rep => rep.OpenLastSession()).Returns("");
            var gbvm = new GameBookViewModel(rs.Object, fileRes.Object, repos.Object);

            Assert.AreEqual("TestBook", gbvm.BookTitle);
            rs.Verify(readsess => readsess.GetBookTitle(), Times.Once);
        }

        [Test]
        public void GoBackTest()
        {
            var rs = new Mock<IReadingSession>();
            var fileRes = new Mock<IChooseResource>();
            var repos = new Mock<IReadingSessionRepository>();
            rs.Setup(readsess => readsess.GetBookTitle()).Returns("TestBook");
            rs.Setup(readsess => readsess.GetCurrentParagraph()).Returns(1);
            rs.Setup(readsess => readsess.GetParagraphChoices(1)).Returns(new Dictionary<string, int>());
            rs.Setup(readsess => readsess.GetHistory()).Returns(new Dictionary<int, string>());
            repos.Setup(rep => rep.OpenLastSession()).Returns("");
            var gbvm = new GameBookViewModel(rs.Object, fileRes.Object, repos.Object);

            gbvm.GoBack.Execute(null);
            rs.Verify(readsess => readsess.GoBackToPrevious(), Times.Once);
        }

        [Test]
        public void SaveOnCloseTest()
        {
            var rs = new Mock<IReadingSession>();
            var fileRes = new Mock<IChooseResource>();
            var repos = new Mock<IReadingSessionRepository>();
            rs.Setup(readsess => readsess.GetBookTitle()).Returns("TestBook");
            rs.Setup(readsess => readsess.GetVisitedParagraphs()).Returns(new List<int>());
            rs.Setup(readsess => readsess.Path).Returns("");
            rs.Setup(readsess => readsess.GetCurrentParagraph()).Returns(1);
            rs.Setup(readsess => readsess.GetParagraphChoices(1)).Returns(new Dictionary<string, int>());
            rs.Setup(readsess => readsess.GetHistory()).Returns(new Dictionary<int, string>());
            repos.Setup(rep => rep.OpenLastSession()).Returns("");
            var gbvm = new GameBookViewModel(rs.Object, fileRes.Object, repos.Object);

            gbvm.SaveOnClose.Execute(null);
            repos.Verify(rep => rep.Save("TestBook", new List<int>(), ""), Times.Once);
        }

        [Test]
        public void SaveWithDefaultBookTest()
        {
            var rs = new Mock<IReadingSession>();
            var fileRes = new Mock<IChooseResource>();
            var repos = new Mock<IReadingSessionRepository>();
            rs.Setup(readsess => readsess.GetBookTitle()).Returns("TestBook");
            rs.Setup(readsess => readsess.GetVisitedParagraphs()).Returns(new List<int>());
            rs.Setup(readsess => readsess.Path).Returns("");
            rs.Setup(readsess => readsess.GetCurrentParagraph()).Returns(1);
            rs.Setup(readsess => readsess.GetParagraphChoices(1)).Returns(new Dictionary<string, int>());
            rs.Setup(readsess => readsess.GetHistory()).Returns(new Dictionary<int, string>());
            rs.Setup(readsess => readsess.IsFakeBook()).Returns(true);
            repos.Setup(rep => rep.OpenLastSession()).Returns("");
            var gbvm = new GameBookViewModel(rs.Object, fileRes.Object, repos.Object);

            gbvm.SaveOnClose.Execute(null);
            repos.Verify(rep => rep.Save("TestBook", new List<int>(), ""), Times.Never);
        }

        [Test]
        public void OpenBook()
        {
            var rs = new Mock<IReadingSession>();
            var fileRes = new Mock<IChooseResource>();
            var repos = new Mock<IReadingSessionRepository>();
            rs.Setup(readsess => readsess.GetBookTitle()).Returns("TestBook");
            rs.Setup(readsess => readsess.GetVisitedParagraphs()).Returns(new List<int>());
            rs.Setup(readsess => readsess.Path).Returns("");
            rs.Setup(readsess => readsess.GetCurrentParagraph()).Returns(1);
            rs.Setup(readsess => readsess.GetParagraphChoices(1)).Returns(new Dictionary<string, int>());
            rs.Setup(readsess => readsess.GetHistory()).Returns(new Dictionary<int, string>());
            rs.Setup(readsess => readsess.IsFakeBook()).Returns(true);
            repos.Setup(rep => rep.OpenLastSession()).Returns("");
            fileRes.SetupGet(fr => fr.ResourceIdentifier).Returns("../../../resources/test.json");
            var gbvm = new GameBookViewModel(rs.Object, fileRes.Object, repos.Object);

            gbvm.LoadBook.Execute(null);
            rs.Verify(ress => ress.IsFakeBook(), Times.AtLeastOnce);
        }

        [Test]
        public void CheckCurrentParagraph()
        {
            var rs = new Mock<IReadingSession>();
            var fileRes = new Mock<IChooseResource>();
            var repos = new Mock<IReadingSessionRepository>();
            rs.Setup(readsess => readsess.GetBookTitle()).Returns("TestBook");
            rs.Setup(readsess => readsess.GetCurrentParagraph()).Returns(1);
            rs.Setup(readsess => readsess.GetParagraphChoices(1)).Returns(new Dictionary<string, int>());
            rs.Setup(readsess => readsess.GetHistory()).Returns(new Dictionary<int, string>());
            repos.Setup(rep => rep.OpenLastSession()).Returns("");
            var gbvm = new GameBookViewModel(rs.Object, fileRes.Object, repos.Object);

            Assert.AreEqual("Paragraph 1", gbvm.CurrentParagraph);
            rs.Verify(readsess => readsess.GetCurrentParagraph(), Times.AtLeastOnce);
        }

        [Test]
        public void CheckParagraphContent()
        {
            var rs = new Mock<IReadingSession>();
            var fileRes = new Mock<IChooseResource>();
            var repos = new Mock<IReadingSessionRepository>();
            rs.Setup(readsess => readsess.GetBookTitle()).Returns("TestBook");
            rs.Setup(readsess => readsess.GetCurrentParagraph()).Returns(1);
            rs.Setup(readsess => readsess.GetParagraphChoices(1)).Returns(new Dictionary<string, int>());
            rs.Setup(readsess => readsess.GetHistory()).Returns(new Dictionary<int, string>());
            rs.Setup(readsess => readsess.GetParagraphContent()).Returns("this is a test");
            repos.Setup(rep => rep.OpenLastSession()).Returns("");
            var gbvm = new GameBookViewModel(rs.Object, fileRes.Object, repos.Object);

            Assert.AreEqual("this is a test", gbvm.ParagraphContent);
            rs.Verify(readsess => readsess.GetParagraphContent(), Times.AtLeastOnce);
        }

        [Test]
        public void CheckWarning()
        {
            var rs = new Mock<IReadingSession>();
            var fileRes = new Mock<IChooseResource>();
            var repos = new Mock<IReadingSessionRepository>();
            rs.Setup(readsess => readsess.GetBookTitle()).Returns("TestBook");
            rs.Setup(readsess => readsess.GetCurrentParagraph()).Returns(1);
            rs.Setup(readsess => readsess.GetParagraphChoices(1)).Returns(new Dictionary<string, int>());
            rs.Setup(readsess => readsess.GetHistory()).Returns(new Dictionary<int, string>());
            rs.Setup(readsess => readsess.WarningMessage).Returns("No message");
            repos.Setup(rep => rep.OpenLastSession()).Returns("");
            var gbvm = new GameBookViewModel(rs.Object, fileRes.Object, repos.Object);

            Assert.AreEqual("No message", gbvm.WarningMessage);
            rs.Verify(readsess => readsess.WarningMessage, Times.AtLeastOnce);
        }

        [Test]
        public void ChoiceVmTest()
        {
            var choiceVm = new ChoiceViewModel("testChoice", 2, new ParameterlessRelayCommand());

            Assert.AreEqual("testChoice (->2)", choiceVm.ChoiceText);
            Assert.AreEqual(2, choiceVm.Destination);
        }

        [Test]
        public void VisitedParagraphsVmTest()
        {
            var visitedParVm = new VisitedParagraphsViewModel(2, "testValue");

            Assert.AreEqual(2, visitedParVm.Index);
            Assert.AreEqual("testValue", visitedParVm.Label);
        }
    }
}