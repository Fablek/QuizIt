using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizIt.Models
{
    public enum QuestionType
    {
        TextAnswer,
        MultipleChoice
    }

    public class FlashcardQuestion
    {
        public string Question { get; set; }
        public QuestionType Type { get; set; }

        public string TextAnswer { get; set; }

        public List<string> Options { get; set; } = new();
        public int CorrectOptionIndex { get; set; }
    }
}
