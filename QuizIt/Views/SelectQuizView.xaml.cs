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
    public partial class SelectQuizView : UserControl
    {
        private readonly Deck _deck;

        public SelectQuizView(Deck deck)
        {
            InitializeComponent();
            _deck = deck;
            FlashcardList.ItemsSource = _deck.Flashcards;
        }

        private void FlashcardList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FlashcardList.SelectedItem is Flashcard selectedFlashcard)
            {
                if (selectedFlashcard.Questions == null || selectedFlashcard.Questions.Count == 0)
                {
                    MessageBox.Show("Ta fiszka nie zawiera żadnych pytań.");
                    return;
                }

                var main = App.Current.MainWindow as MainWindow;
                main.MainContentControl.Content = new QuizView(selectedFlashcard);
            }
        }
    }
}
