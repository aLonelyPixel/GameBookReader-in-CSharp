using System.Collections.Generic;
using System.IO;
using GameBook.Domain;
using GameBook.io;
using NUnit.Framework;

namespace GameBook.Tests.io
{
    [TestFixture]
    public class JsonSessionRepositoryTest
    {
        [Test]
        public void SaveSessionWithCreateBasic()
        {
            JsonSessionRepository jsr = new JsonSessionRepository("../../../resources/fakeSessionCreate.json");

            jsr.Save("testingSave", new List<int>(), "../../../resources/importTest.json");

            Assert.AreEqual("../../../resources/importTest.json", jsr.OpenLastSession());

            File.Delete(@"../../../resources/fakeSession.json");
        }

        [Test]
        public void SaveSessionWithUpdateBasic()
        {
            JsonSessionRepository jsr = new JsonSessionRepository("../../../resources/fakeSessionUpdate.json");

            jsr.Save("testingSave", new List<int>(), "../../../resources/importTest.json");

            Assert.AreEqual("../../../resources/importTest.json", jsr.OpenLastSession());
        }

        [Test]
        public void OpenSession()
        {
            JsonSessionRepository jsr = new JsonSessionRepository("../../../resources/fakeSession.json");

            jsr.Save("testingSave", new List<int>(), "../../../resources/importTest.json");

            Assert.AreEqual(new List<int>(), jsr.Open("testingSave"));
        }
    }
}
