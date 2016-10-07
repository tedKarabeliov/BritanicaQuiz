namespace BritanicaQuiz.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Quizzes")]
    public class Quiz
    {
        public Quiz()
        {
            this.QuestionsSets = new HashSet<QuestionSet>();
            this.QuizEnrolments = new HashSet<QuizEnrolment>();
            this.ChosenTeacherQuizEnrolments = new HashSet<QuizEnrolment>();
        }

        public int Id { get; set; }
        
        public string Name { get; set; }

        public int MaximumPoints { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }
        
        public int? NextQuizId { get; set; }

        [ForeignKey("NextQuizId")]
        public virtual Quiz NextQuiz { get; set; }

        public virtual ICollection<QuestionSet> QuestionsSets { get; set; }

        public virtual ICollection<QuizEnrolment> QuizEnrolments { get; set; }

        public virtual ICollection<QuizEnrolment> ChosenTeacherQuizEnrolments { get; set; }
    }
}
