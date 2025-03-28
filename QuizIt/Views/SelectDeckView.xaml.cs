using System.Windows;
using System.Windows.Controls;
using QuizIt.Models;
using QuizIt.ViewModels;

namespace QuizIt.Views
{
    public partial class SelectDeckView : UserControl
    {
        private readonly MainViewModel _vm;

        public SelectDeckView(MainViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            DeckList.ItemsSource = _vm.Decks;
        }

        private void DeckList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DeckList.SelectedItem is Deck selectedDeck)
            {
                var main = App.Current.MainWindow as MainWindow;
                main.MainContentControl.Content = new SelectQuizView(selectedDeck);
            }
        }
    }
}