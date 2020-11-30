using System.Collections.Generic;
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
        private readonly IReadingSession _readingSession;
        public ObservableCollection<ChoiceViewModel> Choices { get; }
        private ICommand GoToParagraph { get; }
        public ICommand GoBack { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        public GameBookViewModel(IReadingSession readingSession)
        {
            GoToParagraph = ParameterizedRelayCommand<ChoiceViewModel>.From(DoGoToParagraph);
            GoBack = ParameterlessRelayCommand.From(DoGoBack);
            _readingSession = readingSession;
            Choices = new ObservableCollection<ChoiceViewModel>();
            UpdateChoices();
        }

        private void DoGoToParagraph(ChoiceViewModel choice)
        {
            _readingSession.GoToParagraphByChoice(choice.Destination);
            UpdateChoices();
            OnPropertyChanged(nameof(Choices));
            OnPropertyChanged(nameof(CurrentParagraph));
            OnPropertyChanged(nameof(ParagraphContent));
            OnPropertyChanged(nameof(WarningMessage));
        }

        private void DoGoBack()
        {
            _readingSession.GoBackToPrevious();
            UpdateChoices();
            OnPropertyChanged(nameof(Choices));
            OnPropertyChanged(nameof(CurrentParagraph));
            OnPropertyChanged(nameof(ParagraphContent));
            OnPropertyChanged(nameof(WarningMessage));
        }

        private void UpdateChoices()
        {
            Choices.Clear();
            
            foreach (var choice in _readingSession.GetParagraphChoices(_readingSession.GetCurrentParagraph()))
            {
                Choices.Add(new ChoiceViewModel(choice.Key, choice.Value, GoToParagraph));
            }
        }

        public string BookTitle => _readingSession.GetBookTitle();

        public string CurrentParagraph => $"Paragraph {_readingSession.GetCurrentParagraph()}";

        public string ParagraphContent => _readingSession.GetParagraphContent();

        public string WarningMessage => _readingSession.WarningMessage;

        public IDictionary<string, int> GetParagraphChoices(int paragraphIndex) => _readingSession.GetParagraphChoices(paragraphIndex);

        public bool StoryEnded() => _readingSession.HasStoryEnded();

        public IEnumerable<string> GetVisitedParagraphs() => _readingSession.GetVisitedParagraphs();

        public void GoBackToPrevious() => _readingSession.GoBackToPrevious();

        public void GoToVisitedParagraph(string paragraphText) => _readingSession.GoToVisitedParagraph(paragraphText);

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
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
}
