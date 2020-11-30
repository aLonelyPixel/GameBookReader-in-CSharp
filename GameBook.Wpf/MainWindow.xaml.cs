using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GameBook.Domain;

namespace GameBook.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly ViewModel.GameBookViewModel _mpModel;
        public MainWindow()
        {
            InitializeComponent();
            _mpModel = new ViewModel.GameBookViewModel(new ReadingSession(CreateBook()));
            //CurrentParagraph.Content = $"Paragraph {_mpModel.GetCurrentParagraph()}";
            //ParagraphContent.Text = _mpModel.GetParagraphText(_mpModel.GetCurrentParagraph());
            VisitedParagraphs.SelectionChanged += GoToVisitedParagraph;
            GoBack.Click += GoBackToPrevious;
            RefreshParagraphs();
        }

        private void GoToVisitedParagraph(object sender, RoutedEventArgs e)
        {
            if (VisitedParagraphs.Items.Count <= 0) return;
            _mpModel.GoToVisitedParagraph(VisitedParagraphs.SelectedItem.ToString());
            //Warning.Visibility = Visibility.Hidden;
            Refresh();
        }

        private void RefreshParagraphs()
        {
            //Choices.Children.Clear();
            /*foreach (var choiceText in _mpModel.GetParagraphChoices(_mpModel.GetCurrentParagraph()))
            {
                var choiceButton = new Button
                {
                    Margin = new Thickness(100, 5, 100, 5), FontSize = 15, Content = choiceText
                };
                choiceButton.Click += GoToParagraph;
                Choices.Children.Add(choiceButton);
            }*/
        }

        private void GoToParagraph(object sender, RoutedEventArgs e)
        {
            //Warning.Visibility = Visibility.Visible;
            //Warning.Content = _mpModel.GoToParagraphByChoice(Choices.Children.IndexOf((UIElement) sender));
            Refresh();
        }

        private void CheckEndOfStory()
        {
            if (!_mpModel.StoryEnded()) return;
            //Warning.Visibility = Visibility.Visible;
            //Warning.Content += ". Vous avez atteint la fin du livre.";
            //Warning.Background = new SolidColorBrush(Colors.Green);
        }

        private void UpdateComboBox()
        {
            VisitedParagraphs.Items.Clear();
            HashSet<string> set = new HashSet<string>(_mpModel.GetVisitedParagraphs());
            foreach (var paragraph in set)
            {
                VisitedParagraphs.Items.Add(paragraph);
            }
        }

        private void GoBackToPrevious(object sender, RoutedEventArgs e)
        {
            _mpModel.GoBackToPrevious();
            Refresh();
            //Warning.Visibility = Visibility.Hidden;
        }

        private void Refresh()
        {
            //CurrentParagraph.Content = $"Paragraph {_mpModel.GetCurrentParagraph()}";
            //ParagraphContent.Text = _mpModel.GetParagraphText(_mpModel.GetCurrentParagraph());
            //Warning.Background = new SolidColorBrush(Colors.Orange);
            RefreshParagraphs();
            UpdateComboBox();
            CheckEndOfStory();
        }

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
            var myBook = new Book("L'histoire d'un homme qui a soif pendant le confinement", p1, p2, p3, p4);
            return myBook;
        }
    }
}
