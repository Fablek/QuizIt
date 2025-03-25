using QuizIt.Models;
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

namespace QuizIt.Views
{
    public partial class QuizResultView : UserControl
    {
        private readonly Flashcard _flashcard;

        public QuizResultView(int score, int total, Flashcard flashcard)
        {
            InitializeComponent();
            ResultText.Text = $"Twój wynik: {score} / {total}";
            _flashcard = flashcard;
        }

        private void RestartQuiz_Click(object sender, RoutedEventArgs e)
        {
            var main = Application.Current.MainWindow as MainWindow;
            main.MainContentControl.Content = new QuizView(_flashcard);
        }

        private void ReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            var main = Application.Current.MainWindow as MainWindow;
            var vm = main.DataContext as ViewModels.MainViewModel;
            main.MainContentControl.Content = new DecksView(vm);
        }
    }
}
