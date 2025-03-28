﻿using System;
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
using System;
using System.Collections.Generic;
using QuizIt.Models;
using Microsoft.EntityFrameworkCore;

namespace QuizIt.Views
{
    public partial class FlashcardDetailsView : UserControl
    {
        private readonly Flashcard _flashcard;

        public FlashcardDetailsView(Flashcard flashcard)
        {
            InitializeComponent();
            _flashcard = flashcard;
            DataContext = _flashcard;
        }

        private void QuestionTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TextAnswerPanel == null || MultipleChoicePanel == null)
                return;

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

        private void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            string question = QuestionBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(question)) return;

            var selectedType = (QuestionTypeBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            using (var db = new Data.AppDbContext())
            {
                var flashcardInDb = db.Flashcards
                    .Where(f => f.Id == _flashcard.Id)
                    .Include(f => f.Questions)
                    .FirstOrDefault();

                if (flashcardInDb == null)
                {
                    MessageBox.Show("Nie znaleziono fiszki w bazie.");
                    return;
                }

                if (selectedType == "TextAnswer")
                {
                    string textAnswer = TextAnswerBox.Text.Trim();
                    if (string.IsNullOrWhiteSpace(textAnswer)) return;

                    var newQuestion = new FlashcardQuestion
                    {
                        Question = question,
                        Type = QuestionType.TextAnswer,
                        TextAnswer = textAnswer,
                        FlashcardId = _flashcard.Id // DODANE!
                    };

                    flashcardInDb.Questions.Add(newQuestion);
                    db.SaveChanges();

                    _flashcard.Questions.Add(newQuestion);
                    TextAnswerBox.Text = "";
                }
                else if (selectedType == "MultipleChoice")
                {
                    var options = new List<string>
                    {
                        OptionABox.Text.Trim(),
                        OptionBBox.Text.Trim(),
                        OptionCBox.Text.Trim(),
                        OptionDBox.Text.Trim()
                    };

                    var filledOptions = options.Where(o => !string.IsNullOrWhiteSpace(o)).ToList();
                    if (filledOptions.Count < 2)
                    {
                        MessageBox.Show("Wpisz przynajmniej 2 odpowiedzi.");
                        return;
                    }

                    int correctIndex = CorrectOptionComboBox.SelectedIndex;

                    var newQuestion = new FlashcardQuestion
                    {
                        Question = question,
                        Type = QuestionType.MultipleChoice,
                        Options = filledOptions,
                        CorrectOptionIndex = correctIndex,
                        FlashcardId = _flashcard.Id // DODANE!
                    };

                    flashcardInDb.Questions.Add(newQuestion);
                    db.SaveChanges();

                    _flashcard.Questions.Add(newQuestion);
                    OptionABox.Text = OptionBBox.Text = OptionCBox.Text = OptionDBox.Text = "";
                    CorrectOptionComboBox.SelectedIndex = 0;
                }

                QuestionBox.Text = "";
            }
        }
    }
}