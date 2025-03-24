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
        public ObservableCollection<Deck> Decks { get; set; } = new();

        public void AddDeck(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Decks.Add(new Deck
                {
                    Name = name
                });
            }
        }
    }
}
