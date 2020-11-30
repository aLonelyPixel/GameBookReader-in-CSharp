using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using GameBook.Domain;
using GameBook.ViewModel.Annotations;

namespace GameBook.ViewModel
{
    public class GameBookViewModel : INotifyPropertyChanged
    {
        private readonly IReadingSession _readingSession;

        public ObservableCollection<ChoiceViewModel> Choices { get; }

        public GameBookViewModel(IReadingSession readingSession)
        {
            _readingSession = readingSession;
            //_readingSession.GetParagraphChoices(readingSession.GetCurrentParagraph());
            Choices = new ObservableCollection<ChoiceViewModel>(_readingSession.Select(choice => new ChoiceViewModel(choice)));
        }

        //public string GetBookTitle() => _readingSession.GetBookTitle();

        public string BookTitle => _readingSession.GetBookTitle();

        //public int GetCurrentParagraph() => _readingSession.GetCurrentParagraph();

        public string CurrentParagraph => $"Paragraph {_readingSession.GetCurrentParagraph()}";

        //public string GetParagraphText(int paragraphIndex) => _readingSession.GetParagraphText(paragraphIndex);

        public string ParagraphContent => _readingSession.GetParagraphContent();

        public IEnumerable<string> GetParagraphChoices(int paragraphIndex) => _readingSession.GetParagraphChoices(paragraphIndex);

        public string GoToParagraphByChoice(int choiceIndex) => _readingSession.GoToParagraphByChoice(choiceIndex);

        public bool StoryEnded() => _readingSession.HasStoryEnded();

        public IEnumerable<string> GetVisitedParagraphs() => _readingSession.GetVisitedParagraphs();

        public void GoBackToPrevious() => _readingSession.GoBackToPrevious();

        public void GoToVisitedParagraph(string paragraphText) => _readingSession.GoToVisitedParagraph(paragraphText);


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ChoiceViewModel
    {
        public string ChoiceText { get; }
        public ChoiceViewModel(string choice)
        {
            ChoiceText = choice;
        }
    }
}
