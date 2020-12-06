using GameBook.Domain;
using GameBook.io;
using GameBook.Wpf.Views;

namespace GameBook.Wpf.ViewModels
{
    public class GameBookViewModelLocator
    {
        private readonly GameBookViewModel _gameBookViewModel;

        public GameBookViewModelLocator()
        {
            IReadingSession readingSession = new ReadingSession(CreateBook());
            IReadingSessionRepository sessionRepository = new SaveReadingSession();
            _gameBookViewModel = new GameBookViewModel(readingSession, new FileResourceChooser(), sessionRepository);
        }

        public GameBookViewModel GameBookViewModel => _gameBookViewModel;

        private static Book CreateBook()
        {
            var c1 = new Choice("Va chercher de l'eau", 2);
            var c2 = new Choice("Va chercher du coca", 2);
            var c3 = new Choice("Va chercher de la bière", 2);
            var c4 = new Choice("Rentre chez toi", 4);
            var c5 = new Choice("Va au parc", 3);
            var c6 = new Choice("Utilise l'excuse du jogging et dirige-toi vers Delhaize", 2);
            var p1 = new Paragraph(1, "Jim est assoiffé", c1, c2, c3);
            var p2 = new Paragraph(2, "Maintenant ça va, mais Jim a envie de prendre l'air", c4, c5);
            var p3 = new Paragraph(3, "Jim est content car il est sorti de chez lui après un long confinement. Cependant il s'est fait repéré par la police qui veut l'arrêter car Jim n'a pas suivi les mesures du dernier comité de concertation!", c6);
            var p4 = new Paragraph(4, "Jim est finalment hydraté et en sécurité");
            var myBook = new Book("Superman", p1, p2, p3, p4);
            return myBook;
        }
    }
}