using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuizIt.Models;

namespace QuizIt.Views
{
    public partial class QuizView : UserControl
    {
        private readonly Flashcard _flashcard;
        private int _currentIndex = 0;
        private int _score = 0;

        public QuizView(Flashcard flashcard)
        {
            InitializeComponent();
            _flashcard = flashcard;
            ShowQuestion();
        }

        private void ShowQuestion()
        {
            if (_currentIndex >= _flashcard.Questions.Count)
            {
                var result = new QuizResult
                {
                    DeckName = _flashcard.ParentDeckName ?? "Brak nazwy talii",
                    FlashcardTitle = _flashcard.Title,
                    Score = _score,
                    Total = _flashcard.Questions.Count,
                    Date = DateTime.Now
                };

                var mainWindow = Application.Current.MainWindow as MainWindow;
                var viewModel = mainWindow.DataContext as ViewModels.MainViewModel;
                viewModel.Results.Add(result);

                mainWindow.MainContentControl.Content = new QuizResultView(_score, _flashcard.Questions.Count, _flashcard);
                return;
            }

            var q = _flashcard.Questions[_currentIndex];
            QuestionText.Text = q.Question;

            if (q.Type == QuestionType.TextAnswer)
            {
                TextAnswerPanel.Visibility = Visibility.Visible;
                MultipleChoicePanel.Visibility = Visibility.Collapsed;
                TextAnswerBox.Text = "";
            }
            else
            {
                TextAnswerPanel.Visibility = Visibility.Collapsed;
                MultipleChoicePanel.Visibility = Visibility.Visible;

                OptionARadio.Content = q.Options.Count > 0 ? q.Options[0] : "";
                OptionBRadio.Content = q.Options.Count > 1 ? q.Options[1] : "";
                OptionCRadio.Content = q.Options.Count > 2 ? q.Options[2] : "";
                OptionDRadio.Content = q.Options.Count > 3 ? q.Options[3] : "";

                OptionARadio.IsChecked = OptionBRadio.IsChecked = OptionCRadio.IsChecked = OptionDRadio.IsChecked = false;
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            var q = _flashcard.Questions[_currentIndex];
            bool isCorrect = false;

            if (q.Type == QuestionType.TextAnswer)
            {
                var userAnswer = TextAnswerBox.Text.Trim().ToLower();
                var correct = q.TextAnswer?.Trim().ToLower();
                isCorrect = userAnswer == correct;
            }
            else
            {
                int selectedIndex = -1;
                if (OptionARadio.IsChecked == true) selectedIndex = 0;
                else if (OptionBRadio.IsChecked == true) selectedIndex = 1;
                else if (OptionCRadio.IsChecked == true) selectedIndex = 2;
                else if (OptionDRadio.IsChecked == true) selectedIndex = 3;

                isCorrect = selectedIndex == q.CorrectOptionIndex;
            }

            if (isCorrect) _score++;

            _currentIndex++;
            ShowQuestion();
        }
    }
}
