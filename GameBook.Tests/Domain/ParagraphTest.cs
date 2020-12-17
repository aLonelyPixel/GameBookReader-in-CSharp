using System.Collections.Generic;
using GameBook.Domain;
using NUnit.Framework;

namespace GameBook.Tests.Domain
{
    [TestFixture]
    public class ParagraphTest
    {
        [Test]
        public void KnowsItsText()
        {
            var choice1 = new Choice("choice1", 2);
            var choiceList = new List<Choice> {choice1};
            var paragraph1 = new Paragraph(1, "testing this", choiceList);
            Assert.AreEqual("testing this", paragraph1.Text);
        }

        [Test]
        public void InitByList()
        {
            var choice1 = new Choice("choice1", 2);
            var choice2 = new Choice("choice2", 3);
            var choice3 = new Choice("choice3", 9);
            var choiceList = new List<Choice> {choice1, choice2, choice3};
            var paragraph1 = new Paragraph(1, "testing this", choiceList);
            Assert.AreEqual("testing this", paragraph1.Text);
        }

        [Test]
        public void KnowsAmountOfChoices()
        {
            var choice1 = new Choice("choice1", 2);
            var choice2 = new Choice("choice2", 3);
            var choice3 = new Choice("choice3", 9);
            var choiceList = new List<Choice> { choice1, choice2, choice3 };
            var paragraph1 = new Paragraph(1, "testing this", choiceList);
            Assert.AreEqual(3, paragraph1.Choices.Count);
            Assert.IsFalse(paragraph1.IsTerminal());
        }

        [Test]
        public void KnowsItsTerminal()
        {
            var paragraph1 = new Paragraph(1, "testing this" , new List<Choice>());
            Assert.IsTrue(paragraph1.IsTerminal());
        }

        [Test]
        public void KnowsItsIndex()
        {
            var paragraph1 = new Paragraph(7, "testing this", new List<Choice>());
            Assert.AreEqual(7, paragraph1.Index);
        }

        [Test]
        public void GetsChoices()
        {
            var choice1 = new Choice("choice1", 2);
            var choice2 = new Choice("choice2", 3);
            var choice3 = new Choice("choice3", 9);
            var choiceList = new List<Choice> { choice1, choice2, choice3 };
            var paragraph1 = new Paragraph(1, "testing this", choiceList);
            IDictionary<string, int> choicesList = new Dictionary<string, int>();
            choicesList.Add("choice1", 2);
            choicesList.Add("choice2", 3);
            choicesList.Add("choice3", 9);
            
            Assert.AreEqual(choicesList, paragraph1.GetChoices());
        }

        [Test]
        public void GetsChoicesDestination()
        {
            var choice1 = new Choice("choice1", 2);
            var choice2 = new Choice("choice2", 3);
            var choice3 = new Choice("choice3", 9);
            var choiceList = new List<Choice> { choice1, choice2, choice3 };
            var paragraph1 = new Paragraph(1, "testing this", choiceList);
            Assert.AreEqual(2, paragraph1.GetChoiceDestination(0));
            Assert.AreEqual(3, paragraph1.GetChoiceDestination(1));
            Assert.AreEqual(9, paragraph1.GetChoiceDestination(2));
            Assert.AreEqual(-1, paragraph1.GetChoiceDestination(-3));
        }

        [Test]
        public void GetParagraphLabel()
        {
            var paragraph1 = new Paragraph(1, "testing this", new List<Choice>());
            Assert.AreEqual("testing this ...", paragraph1.GetLabel());
        }
    }
}