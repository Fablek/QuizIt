using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizIt.Models
{
    public class Flashcard
    {
        public string Title { get; set; }
        public ObservableCollection<FlashcardQuestion> Questions { get; set; } = new();

        public override string ToString() => Title;
    }
}
