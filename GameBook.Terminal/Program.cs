using GameBook.Commands;
using GameBook.Domain;

namespace GameBook.Terminal
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var myBook = CreateBook();
            var mpModel = new MainPresentationModel(myBook);
            var exitCommand = new ExitCommand();
            var readBookCommand = new ReadBookCommand(mpModel);
            var myTerminal = new Terminal(exitCommand, readBookCommand);

            myTerminal.Loop();
        }

        private static Book CreateBook()
        {
            var c1 = new Choice("Va chercher de l'eau", 2);
            var c2 = new Choice("Va chercher du coca", 2);
            var c3 = new Choice("Va chercher de la bière", 2);
            var c4 = new Choice("Rentre chez toi", 3);
            var c5 = new Choice("Va au parc", 1);
            var p1 = new Paragraph(1, "Jim est assoiffé...", c1, c2, c3);
            var p2 = new Paragraph(2, "Maintenant ça va :)", c4, c5);
            var p3 = new Paragraph(3, "Jim est finalment hydraté");
            var myBook = new Book("L'histoire d'un homme qui a soif mais ne sait pas quoi faire parce qu'il manque de confiance en lui", p1, p2, p3);
            return myBook;
        }
    }
}
