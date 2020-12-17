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
            IReadingSession readingSession = new ReadingSession();
            IReadingSessionRepository sessionRepository = new JsonSessionRepository();
            _gameBookViewModel = new GameBookViewModel(readingSession, new FileResourceChooser(), sessionRepository);
        }

        public GameBookViewModel GameBookViewModel => _gameBookViewModel;
    }
}