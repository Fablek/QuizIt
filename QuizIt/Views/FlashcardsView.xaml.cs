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
                _deck.Flashcards.Add(new Flashcard { Title = title, ParentDeckName = _deck.Name });
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