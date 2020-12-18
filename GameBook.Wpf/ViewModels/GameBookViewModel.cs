using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GameBook.Domain;
using GameBook.io;
using GameBook.ViewModel.Annotations;
using GameBook.Wpf.ViewModels.Command;

namespace GameBook.Wpf.ViewModels
{
    public class GameBookViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IReadingSession _readingSession;
        private readonly IChooseResource _chooser;
        private readonly IReadingSessionRepository _sessionRepository;
        public ObservableCollection<ChoiceViewModel> Choices { get; }
        public ObservableCollection<VisitedParagraphsViewModel> VisitedParagraphs { get; }
        public ICommand LoadBook { get; set; }
        private ICommand GoToParagraph { get; set; }
        public ICommand GoBack { get; set; }
        public ICommand SaveOnClose { get; set; }

        public GameBookViewModel(IReadingSession readingSession, IChooseResource chooser, IReadingSessionRepository sessionRepository)
        {
            InitCommands();
            _readingSession = readingSession;
            _chooser = chooser;
            _sessionRepository = sessionRepository;
            Choices = new ObservableCollection<ChoiceViewModel>();
            VisitedParagraphs = new ObservableCollection<VisitedParagraphsViewModel>();
            OpenLastSession();
            UpdateChoices();
            UpdateVisitedParagraphs();
        }

        private void InitCommands()
        {
            LoadBook = ParameterlessRelayCommand.From((DoOpen));
            GoToParagraph = ParameterizedRelayCommand<ChoiceViewModel>.From(DoGoToParagraph);
            GoBack = ParameterlessRelayCommand.From(DoGoBack);
            SaveOnClose = ParameterlessRelayCommand.From(DoSaveOnClose);
        }

        private void OpenLastSession()
        {
            if (_sessionRepository.OpenLastSession().Equals("")) return;
            IBookLoader loader = new JsonLoader();
            _readingSession.SetBook(loader.LoadBook(_sessionRepository.OpenLastSession()), _sessionRepository.OpenLastSession());
            var lastSession = _sessionRepository.Open(_readingSession.GetBookTitle());
            if (lastSession == null || lastSession.Count == 0) return;
            _readingSession.OpenLastSession(lastSession);
        }

        private void OpenSavedSession()
        {
            var lastSession = _sessionRepository.Open(_readingSession.GetBookTitle());
            if (lastSession == null || lastSession.Count == 0) return;
            _readingSession.OpenLastSession(lastSession);
        }

        private void DoGoToParagraph(ChoiceViewModel choice)
        {
            _readingSession.GoToParagraphByChoice(choice.Destination);
            Refresh();
        }

        private void DoGoToKnownParagraph(VisitedParagraphsViewModel visitedParagraph)
        {
            _readingSession.GoToVisitedParagraph(visitedParagraph.Index);
            Refresh();
        }

        private void DoGoBack()
        {
            _readingSession.GoBackToPrevious();
            Refresh();
        }

        private void DoOpen()
        {
            if (!_readingSession.IsFakeBook()) DoSaveOnClose();
            var path = _chooser.ResourceIdentifier;
            IBookLoader loader = new JsonLoader();
            _readingSession.SetBook(loader.LoadBook(path), path);
            OpenSavedSession();
            Refresh();
        }

        private void DoSaveOnClose()
        {
            if (_readingSession.IsFakeBook()) return;
            _sessionRepository.Save(_readingSession.GetBookTitle(), _readingSession.GetVisitedParagraphs(), _readingSession.Path);
        }

        private void Refresh()
        {
            UpdateChoices();
            UpdateVisitedParagraphs();
            OnPropertyChanged(nameof(BookTitle));
            OnPropertyChanged(nameof(Choices));
            OnPropertyChanged(nameof(CurrentParagraph));
            OnPropertyChanged(nameof(ParagraphContent));
            OnPropertyChanged(nameof(WarningMessage));
            OnPropertyChanged(nameof(VisitedParagraphs));
        }

        private void UpdateChoices()
        {
            Choices.Clear();
            foreach (var (key, value) in _readingSession.GetParagraphChoices(_readingSession.GetCurrentParagraph()))
            {
                Choices.Add(new ChoiceViewModel(key, value, GoToParagraph));
            }
        }

        private void UpdateVisitedParagraphs()
        {
            VisitedParagraphs.Clear();
            foreach (var (key, value) in _readingSession.GetHistory())
            {
                VisitedParagraphs.Add(new VisitedParagraphsViewModel(key, value));
            }
        }

        public string BookTitle => _readingSession.GetBookTitle();

        public string CurrentParagraph => $"Paragraph {_readingSession.GetCurrentParagraph()}";

        public string ParagraphContent => _readingSession.GetParagraphContent();

        public string WarningMessage => _readingSession.WarningMessage;

        public VisitedParagraphsViewModel SelectedParagraph
        {
            set
            {
                if (value == null) return;
                DoGoToKnownParagraph(value);
            }
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ChoiceViewModel
    {
        public string ChoiceText { get; }
        public int Destination { get; }
        public ICommand GoToParagraph { get; }
        public ChoiceViewModel(string text, int destination, ICommand goToParagraph)
        {
            ChoiceText = $"{text} (->{destination})";
            Destination = destination;
            GoToParagraph = goToParagraph;
        }
    }

    public class VisitedParagraphsViewModel
    {
        public string Label { get; }
        public int Index { get; }
        public VisitedParagraphsViewModel(int key, string value)
        {
            Index = key;
            Label = value;
        }
    }
}
