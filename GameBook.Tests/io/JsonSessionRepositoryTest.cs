using System.Collections.Generic;
using GameBook.io;
using NUnit.Framework;

namespace GameBook.Tests.io
{
    [TestFixture]
    public class JsonSessionRepositoryTest
    {
        [Test]
        public void SaveSessionBasic()
        {
            JsonSessionRepository jsr = new JsonSessionRepository();

            jsr.Save("testingSave", new List<int>(), "../../../resources/test.json");


        }

        [Test]
        public void LoadLastSessionBasic()
        {
            JsonSessionRepository jsr = new JsonSessionRepository();

            string lastSession = jsr.OpenLastSession();

            Assert.AreEqual("", lastSession);
        }
    }
}
