namespace BritanicaQuiz.Model
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CompletedQuizzesHistory")]
    public class CompletedQuizzesHistory
    {
        public int Id { get; set; }

        public int QuizEnrolmentId { get; set; }

        public virtual QuizEnrolment QuizEnrolment { get; set; }

        public int QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }

        public int Points { get; set; }
    }
}
