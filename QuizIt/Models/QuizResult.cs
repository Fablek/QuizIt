using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizIt.Models
{
    public class QuizResult
    {
        public string DeckName { get; set; }
        public string FlashcardTitle { get; set; }
        public int Score { get; set; }
        public int Total { get; set; }
        public DateTime Date { get; set; }
        public double Percentage => Total > 0 ? (double)Score / Total * 100 : 0;
    }
}

