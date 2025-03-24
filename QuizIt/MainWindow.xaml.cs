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

        _mainViewModel = new MainViewModel();
        DataContext = _mainViewModel;

        MainContentControl.Content = new DecksView(_mainViewModel);
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
}