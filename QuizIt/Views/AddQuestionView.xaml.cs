using QuizIt.Models;
using QuizIt.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;

namespace QuizIt.Views
{
    public partial class AddQuestionView : UserControl
    {
        private readonly Flashcard _flashcard;

        public AddQuestionView(Flashcard flashcard)
        {
            InitializeComponent();
            _flashcard = flashcard;
        }

        private void QuestionTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (QuestionTypeBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            TextAnswerPanel.Visibility = selected == "TextAnswer" ? Visibility.Visible : Visibility.Collapsed;
            MultipleChoicePanel.Visibility = selected == "MultipleChoice" ? Visibility.Visible : Visibility.Collapsed;
        }

        private void SaveQuestion_Click(object sender, RoutedEventArgs e)
        {
            var selectedType = (QuestionTypeBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            using (var db = new AppDbContext())
            {
                var flashcardInDb = db.Flashcards
                    .Include(f => f.Questions)
                    .FirstOrDefault(f => f.Id == _flashcard.Id);

                if (flashcardInDb == null)
                {
                    MessageBox.Show("Nie znaleziono fiszki w bazie.");
                    return;
                }

                FlashcardQuestion newQuestion;

                if (selectedType == "TextAnswer")
                {
                    string answer = TextAnswerBox.Text.Trim();
                    if (string.IsNullOrWhiteSpace(answer)) return;

                    newQuestion = new FlashcardQuestion
                    {
                        Question = QuestionBox.Text.Trim(),
                        Type = QuestionType.TextAnswer,
                        TextAnswer = answer,
                        FlashcardId = _flashcard.Id
                    };
                }
                else
                {
                    var options = new List<string>
            {
                OptionABox.Text.Trim(),
                OptionBBox.Text.Trim(),
                OptionCBox.Text.Trim(),
                OptionDBox.Text.Trim()
            }.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

                    if (options.Count < 2)
                    {
                        MessageBox.Show("Wpisz przynajmniej 2 odpowiedzi.");
                        return;
                    }

                    newQuestion = new FlashcardQuestion
                    {
                        Question = QuestionBox.Text.Trim(),
                        Type = QuestionType.MultipleChoice,
                        Options = options,
                        CorrectOptionIndex = CorrectOptionComboBox.SelectedIndex,
                        FlashcardId = _flashcard.Id
                    };
                }

                // Zapis do bazy
                db.FlashcardQuestions.Add(newQuestion);
                db.SaveChanges();

                // Dodanie do aktualnej fiszki w pamięci
                _flashcard.Questions.Add(newQuestion);
            }

            MessageBox.Show("Dodano pytanie!");
            var main = Application.Current.MainWindow as MainWindow;
            main.MainContentControl.Content = new FlashcardDetailsView(_flashcard);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ReturnToDetailsView();
        }

        private void ReturnToDetailsView()
        {
            var main = App.Current.MainWindow as MainWindow;
            main.MainContentControl.Content = new FlashcardDetailsView(_flashcard);
        }
    }
}
