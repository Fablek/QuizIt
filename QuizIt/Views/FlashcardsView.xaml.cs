using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using QuizIt.Data;
using QuizIt.Models;
using Microsoft.VisualBasic;

namespace QuizIt.Views
{
    public partial class FlashcardsView : UserControl
    {
        private readonly Deck _deck;

        public FlashcardsView(Deck deck)
        {
            InitializeComponent();
            _deck = deck;
            DataContext = _deck;
        }

        private void AddFlashcard_Click(object sender, RoutedEventArgs e)
        {
            string title = NewFlashcardTitleBox.Text.Trim();
            if (!string.IsNullOrEmpty(title))
            {
                var flashcard = new Flashcard
                {
                    Title = title,
                    ParentDeckName = _deck.Name,
                    DeckId = _deck.Id
                };

                using (var db = new AppDbContext())
                {
                    db.Flashcards.Add(flashcard);
                    db.SaveChanges();
                }

                ReloadView();
                NewFlashcardTitleBox.Text = "";
            }
        }

        private void OpenFlashcard_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is Flashcard selectedFlashcard)
            {
                var main = App.Current.MainWindow as MainWindow;
                main.MainContentControl.Content = new FlashcardDetailsView(selectedFlashcard);
            }
        }

        private void EditFlashcard_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is Flashcard flashcard)
            {
                string newTitle = Interaction.InputBox(
                    "Podaj nowy tytuł fiszki:", "Edytuj fiszkę", flashcard.Title);

                if (!string.IsNullOrWhiteSpace(newTitle))
                {
                    using (var db = new AppDbContext())
                    {
                        var flashInDb = db.Flashcards.FirstOrDefault(f => f.Id == flashcard.Id);
                        if (flashInDb != null)
                        {
                            flashInDb.Title = newTitle;
                            db.SaveChanges();
                        }
                    }

                    ReloadView();
                }
            }
        }

        private void DeleteFlashcard_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is Flashcard flashcard)
            {
                var result = MessageBox.Show($"Czy na pewno chcesz usunąć fiszkę '{flashcard.Title}'?",
                                             "Usuń fiszkę", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    using (var db = new AppDbContext())
                    {
                        var toRemove = db.Flashcards.FirstOrDefault(f => f.Id == flashcard.Id);
                        if (toRemove != null)
                        {
                            db.Flashcards.Remove(toRemove);
                            db.SaveChanges();
                        }
                    }

                    ReloadView();
                }
            }
        }

        private void ReloadView()
        {
            var main = Application.Current.MainWindow as MainWindow;
            var viewModel = main.DataContext as ViewModels.MainViewModel;
            viewModel.ReloadAllData();

            var updatedDeck = viewModel.Decks.FirstOrDefault(d => d.Id == _deck.Id);
            if (updatedDeck != null)
            {
                main.MainContentControl.Content = new FlashcardsView(updatedDeck);
            }
        }
    }
}
