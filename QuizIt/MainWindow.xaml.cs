using QuizIt.ViewModels;
using System.Collections.ObjectModel;
using System.Text;
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
using QuizIt.Views;
using QuizIt.Data;

namespace QuizIt;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private MainViewModel _mainViewModel;
    public ObservableCollection<Deck> Decks { get; set; }

    public MainWindow()
    {
        InitializeComponent();

        using (var db = new AppDbContext())
        {
            db.Database.EnsureCreated();
        }

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
        if (DataContext is MainViewModel vm && vm.Decks.Count > 0)
        {
            MainContentControl.Content = new SelectDeckView(vm);
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
        var viewModel = DataContext as MainViewModel;
        MainContentControl.Content = new DecksView(viewModel);
    }

    private void ShowAddDeckView_Click(object sender, RoutedEventArgs e)
    {
        var viewModel = DataContext as MainViewModel;
        MainContentControl.Content = new AddDeckView(viewModel);
    }

    private void ShowSettingsView_Click(object sender, RoutedEventArgs e)
    {
        MainContentControl.Content = new SettingsView();
    }
}