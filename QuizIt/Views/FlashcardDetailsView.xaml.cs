using System.Linq;
using System.Windows;
using System.Windows.Controls;
using QuizIt.Models;
using Microsoft.EntityFrameworkCore;

namespace QuizIt.Views
{
    public partial class FlashcardDetailsView : UserControl
    {
        private readonly Flashcard _flashcard;

        public FlashcardDetailsView(Flashcard flashcard)
        {
            InitializeComponent();
            _flashcard = flashcard;
            DataContext = _flashcard;
        }

        private void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            var main = App.Current.MainWindow as MainWindow;
            main.MainContentControl.Content = new AddQuestionView(_flashcard);
        }

        private void EditQuestion_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.Tag is FlashcardQuestion question)
            {
                var main = App.Current.MainWindow as MainWindow;
                main.MainContentControl.Content = new EditQuestionView(_flashcard, question);
            }
        }

        private void DeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.Tag is FlashcardQuestion question)
            {
                var result = MessageBox.Show("Czy na pewno chcesz usunąć to pytanie?", "Potwierdzenie", MessageBoxButton.YesNo);
                if (result != MessageBoxResult.Yes) return;

                using (var db = new Data.AppDbContext())
                {
                    var questionInDb = db.FlashcardQuestions.FirstOrDefault(q => q.Id == question.Id);
                    if (questionInDb != null)
                    {
                        db.FlashcardQuestions.Remove(questionInDb);
                        db.SaveChanges();
                    }
                }

                _flashcard.Questions.Remove(question);
            }
        }
    }
}