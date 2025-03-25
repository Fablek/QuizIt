using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using QuizIt.Models;
using System.ComponentModel;

namespace QuizIt.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Deck> Decks { get; set; } = new();
        public string Username => "👤 " + Properties.Settings.Default.Username;
        public string Greeting => $"👋 Cześć, {Properties.Settings.Default.Username}!";

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
    }
}
