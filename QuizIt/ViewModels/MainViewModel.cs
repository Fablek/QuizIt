using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using QuizIt.Models;
using System.ComponentModel;
using QuizIt.Data;
using Microsoft.EntityFrameworkCore;

namespace QuizIt.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Deck> Decks { get; set; } = new();
        public ObservableCollection<QuizResult> Results { get; set; }
        public string Username => "👤 " + Properties.Settings.Default.Username;
        public string Greeting => $"👋 Cześć, {Properties.Settings.Default.Username}!";

        public MainViewModel()
        {
            LoadDataFromDatabase();
        }

        public void LoadDataFromDatabase()
        {
            Decks.Clear();

            using (var db = new AppDbContext())
            {
                var decks = db.Decks
                    .Include(d => d.Flashcards)
                    .ThenInclude(f => f.Questions)
                    .ToList();

                foreach (var deck in decks)
                {
                    Decks.Add(deck);
                }

                Results = new ObservableCollection<QuizResult>(db.QuizResults.ToList());
                OnPropertyChanged(nameof(Results));
            }
        }

        public void AddDeck(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                var newDeck = new Deck { Name = name };

                using (var db = new AppDbContext())
                {
                    db.Decks.Add(newDeck);
                    db.SaveChanges();
                }

                Decks.Add(newDeck);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshUsername()
        {
            OnPropertyChanged(nameof(Username));
            OnPropertyChanged(nameof(Greeting));
        }

        public void ReloadAllData()
        {
            Decks.Clear();
            Results.Clear();

            using (var db = new AppDbContext())
            {
                var decks = db.Decks
                    .Include(d => d.Flashcards)
                    .ThenInclude(f => f.Questions)
                    .ToList();

                foreach (var deck in decks)
                    Decks.Add(deck);

                foreach (var result in db.QuizResults)
                    Results.Add(result);
            }

            OnPropertyChanged(nameof(Decks));
            OnPropertyChanged(nameof(Results));
        }
    }
}
