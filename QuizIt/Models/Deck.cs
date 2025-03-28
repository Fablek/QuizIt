using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizIt.Models
{
    public class Deck
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<Flashcard> Flashcards { get; set; } = new();

        public override string ToString() => Name;
    }
}
