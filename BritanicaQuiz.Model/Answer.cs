using System.Collections.Generic;
namespace BritanicaQuiz.Model
{
    public class Answer
    {
        public Answer()
        {
            this.QuizResults = new HashSet<QuizResult>();
        }

        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        public bool CaseSensitive { get; set; }

        public int Points { get; set; }

        public int NegativePoints { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public virtual ICollection<QuizResult> QuizResults { get; set; }
    }
}
