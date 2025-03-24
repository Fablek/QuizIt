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

namespace QuizIt;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public ObservableCollection<Deck> Decks { get; set; }

    public MainWindow()
    {
        InitializeComponent();

        Decks = new ObservableCollection<Deck>
        {
            new Deck { Name = "Angielski", FlashcardCount = 20 },
            new Deck { Name = "Biologia", FlashcardCount = 15 },
            new Deck { Name = "Geografia", FlashcardCount = 10 },
        };

        DataContext = this;
    }
}