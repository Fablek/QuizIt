using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizIt
{
    public class Deck
    {
        public string Name { get; set; }
        public int FlashcardCount { get; set; }

        public override string ToString()
        {
            return $"▸ {Name} – {FlashcardCount} talie";
        }
    }
}
