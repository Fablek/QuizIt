using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using QuizIt.Models;
using QuizIt.Data;
using Microsoft.EntityFrameworkCore;

namespace QuizIt.Views
{
    public partial class EditQuestionView : UserControl
    {
        private readonly Flashcard _flashcard;
        private readonly FlashcardQuestion _question;

        public EditQuestionView(Flashcard flashcard, FlashcardQuestion question)
        {
            InitializeComponent();
            _flashcard = flashcard;
            _question = question;

            LoadData();
        }

        private void EditQuestion_Click(object sender, RoutedEventArgs e)
        {
            var question = ((Button)sender).Tag as FlashcardQuestion;
            var main = App.Current.MainWindow as MainWindow;
            main.MainContentControl.Content = new EditQuestionView(_flashcard, question);
        }

        private void LoadData()
        {
            QuestionBox.Text = _question.Question;

            if (_question.Type == QuestionType.TextAnswer)
            {
                QuestionTypeBox.SelectedIndex = 0;
                TextAnswerPanel.Visibility = Visibility.Visible;
                MultipleChoicePanel.Visibility = Visibility.Collapsed;
                TextAnswerBox.Text = _question.TextAnswer;
            }
            else
            {
                QuestionTypeBox.SelectedIndex = 1;
                TextAnswerPanel.Visibility = Visibility.Collapsed;
                MultipleChoicePanel.Visibility = Visibility.Visible;

                if (_question.Options.Count > 0) OptionABox.Text = _question.Options.ElementAtOrDefault(0);
                if (_question.Options.Count > 1) OptionBBox.Text = _question.Options.ElementAtOrDefault(1);
                if (_question.Options.Count > 2) OptionCBox.Text = _question.Options.ElementAtOrDefault(2);
                if (_question.Options.Count > 3) OptionDBox.Text = _question.Options.ElementAtOrDefault(3);

                CorrectOptionComboBox.SelectedIndex = _question.CorrectOptionIndex;
            }
        }

        private void QuestionTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (QuestionTypeBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (selected == "TextAnswer")
            {
                TextAnswerPanel.Visibility = Visibility.Visible;
                MultipleChoicePanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                TextAnswerPanel.Visibility = Visibility.Collapsed;
                MultipleChoicePanel.Visibility = Visibility.Visible;
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                var questionInDb = db.FlashcardQuestions.FirstOrDefault(q => q.Id == _question.Id);
                if (questionInDb == null)
                {
                    MessageBox.Show("Nie znaleziono pytania w bazie.");
                    return;
                }

                questionInDb.Question = QuestionBox.Text.Trim();

                var selectedType = (QuestionTypeBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                if (selectedType == "TextAnswer")
                {
                    questionInDb.Type = QuestionType.TextAnswer;
                    questionInDb.TextAnswer = TextAnswerBox.Text.Trim();
                    questionInDb.Options = new List<string>();
                    questionInDb.CorrectOptionIndex = 0;
                }
                else
                {
                    questionInDb.Type = QuestionType.MultipleChoice;
                    questionInDb.Options = new List<string>
                    {
                        OptionABox.Text.Trim(),
                        OptionBBox.Text.Trim(),
                        OptionCBox.Text.Trim(),
                        OptionDBox.Text.Trim()
                    }.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

                    questionInDb.CorrectOptionIndex = CorrectOptionComboBox.SelectedIndex;
                    questionInDb.TextAnswer = "";
                }

                db.SaveChanges();
            }

            MessageBox.Show("Zapisano zmiany.");
            var main = Application.Current.MainWindow as MainWindow;
            var vm = main.DataContext as ViewModels.MainViewModel;
            vm.ReloadAllData();
            main.MainContentControl.Content = new FlashcardDetailsView(_question.Flashcard);
        }
    }
}
