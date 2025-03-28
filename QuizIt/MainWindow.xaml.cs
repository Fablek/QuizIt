using QuizIt.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using QuizIt.Views;
using QuizIt.Models;

namespace QuizIt
{
    public partial class MainWindow : Window
    {
        private MainViewModel _mainViewModel;

        public MainWindow()
        {
            InitializeComponent();

            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.Username))
            {
                Properties.Settings.Default.Username = Environment.UserName;
                Properties.Settings.Default.Save();
            }

            ((App)Application.Current).ApplyTheme(Properties.Settings.Default.DarkMode);

            _mainViewModel = new MainViewModel();
            DataContext = _mainViewModel;

            MainContentControl.Content = new DecksView(_mainViewModel);
        }

        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            if (_mainViewModel.Decks.Count > 0)
            {
                MainContentControl.Content = new SelectDeckView(_mainViewModel);
            }
            else
            {
                MessageBox.Show("Brak talii do rozpoczęcia quizu.");
            }
        }

        private void ShowResultsView_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.Content = new ResultsView();
        }

        private void ShowDecksView_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.Content = new DecksView(_mainViewModel);
        }

        private void ShowAddDeckView_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.Content = new AddDeckView(_mainViewModel);
        }

        private void ShowSettingsView_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.Content = new SettingsView();
        }
    }
}
