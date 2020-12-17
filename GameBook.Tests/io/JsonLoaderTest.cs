using GameBook.Domain;
using GameBook.io;
using NUnit.Framework;

namespace GameBook.Tests.io
{
    [TestFixture]
    public class JsonLoaderTest
    {
        [Test]
        public void LoadBasic()
        {
            var jl = new JsonLoader();

            var newBook = jl.LoadBook("../../../resources/importTest.json");

            Assert.AreEqual("L'histoire d'un homme qui a soif pendant le confinement", newBook.Name);
        }
    }
}