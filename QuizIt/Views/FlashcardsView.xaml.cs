using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using QuizIt.Data;
using QuizIt.Models;

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

                var main = Application.Current.MainWindow as MainWindow;
                var viewModel = main.DataContext as ViewModels.MainViewModel;
                viewModel.ReloadAllData();

                var updatedDeck = viewModel.Decks.FirstOrDefault(d => d.Id == _deck.Id);
                if (updatedDeck != null)
                {
                    main.MainContentControl.Content = new FlashcardsView(updatedDeck);
                }

                NewFlashcardTitleBox.Text = "";
            }
        }

        private void Flashcard_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (((ListBox)sender).SelectedItem is Flashcard selectedFlashcard)
            {
                var main = App.Current.MainWindow as MainWindow;
                main.MainContentControl.Content = new FlashcardDetailsView(selectedFlashcard);
            }
        }
    }
}
