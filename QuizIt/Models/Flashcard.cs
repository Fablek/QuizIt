﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizIt.Models
{
    public class Flashcard
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int DeckId { get; set; }
        public Deck Deck { get; set; }
        public ObservableCollection<FlashcardQuestion> Questions { get; set; } = new();
        public string ParentDeckName { get; set; }

        public override string ToString() => Title;
    }
}
