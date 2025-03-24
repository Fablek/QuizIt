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
using QuizIt.ViewModels;

namespace QuizIt.Views
{
    /// <summary>
    /// Logika interakcji dla klasy DecksView.xaml
    /// </summary>
    public partial class DecksView : UserControl
    {
        private MainViewModel _viewModel;

        public DecksView(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private void DecksListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is Deck selectedDeck)
            {
                var mainWindow = App.Current.MainWindow as MainWindow;
                mainWindow.MainContentControl.Content = new FlashcardsView(selectedDeck);
            }
        }
    }
}
