using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GameBook.Domain;
using GameBook.ViewModel.Annotations;
using GameBook.ViewModel.Command;

namespace GameBook.ViewModel
{
    public class GameBookViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IReadingSession _readingSession;
        public ObservableCollection<ChoiceViewModel> Choices { get; }
        public ObservableCollection<VisitedParagraphsViewModel> VisitedParagraphs { get; }
        private ICommand GoToParagraph { get; }
        public ICommand GoBack { get; }
        public ICommand Open { get; }
        
        public GameBookViewModel(IReadingSession readingSession)
        {
            GoToParagraph = ParameterizedRelayCommand<ChoiceViewModel>.From(DoGoToParagraph);
            GoBack = ParameterlessRelayCommand.From(DoGoBack);
            Open = ParameterlessRelayCommand.From(DoOpen);
            _readingSession = readingSession;
            Choices = new ObservableCollection<ChoiceViewModel>();
            VisitedParagraphs = new ObservableCollection<VisitedParagraphsViewModel>();
            UpdateChoices();
            UpdateVisitedParagraphs();
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
            
        }
        private void Refresh()
        {
            UpdateChoices();
            UpdateVisitedParagraphs();
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
