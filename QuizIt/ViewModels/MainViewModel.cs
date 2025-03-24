using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using QuizIt.Models;

namespace QuizIt.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<Deck> Decks { get; set; }

        public MainViewModel()
        {
            Decks = new ObservableCollection<Deck>
            {
                new Deck { Name = "Angielski", FlashcardCount = 20 },
                new Deck { Name = "Biologia", FlashcardCount = 15 },
                new Deck { Name = "Geografia", FlashcardCount = 10 },
            };
        }

        public void AddDeck(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Decks.Add(new Deck
                {
                    Name = name,
                    FlashcardCount = 0
                });
            }
        }
    }
}
