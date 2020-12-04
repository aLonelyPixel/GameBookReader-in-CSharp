using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
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
        private IReadingSessionRepository _sessionRepository;
        public ObservableCollection<ChoiceViewModel> Choices { get; }
        public ObservableCollection<VisitedParagraphsViewModel> VisitedParagraphs { get; }
        private ICommand GoToParagraph { get; }
        public ICommand GoBack { get; }
        public ICommand Open { get; }
        public ICommand SaveOnClose { get; }

        public GameBookViewModel(IReadingSession readingSession, IChooseResource chooser, IReadingSessionRepository sessionRepository)
        {
            GoToParagraph = ParameterizedRelayCommand<ChoiceViewModel>.From(DoGoToParagraph);
            GoBack = ParameterlessRelayCommand.From(DoGoBack);
            Open = ParameterlessRelayCommand.From(DoOpen);
            SaveOnClose = ParameterlessRelayCommand.From(DoSaveOnClose);
            _readingSession = readingSession;
            _chooser = chooser;
            _sessionRepository = sessionRepository;
            Choices = new ObservableCollection<ChoiceViewModel>();
            VisitedParagraphs = new ObservableCollection<VisitedParagraphsViewModel>();
            OpenLastSession();
            UpdateChoices();
            UpdateVisitedParagraphs();
        }

        private void OpenLastSession()
        {
            IList<int>[] myList = _sessionRepository.Open(_readingSession);
            if (myList == null) return;
            _readingSession.SetCurrentParagraph(myList[0].Last());
            _readingSession.SetVisitedParagraphs(myList[1]);
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
            using (TextReader fileStream = File.OpenText(_chooser.ResourceIdentifier))
            {

            }
        }

        private void DoSaveOnClose()
        {
            _sessionRepository.Save(_readingSession);
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
