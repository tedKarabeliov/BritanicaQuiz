namespace BritanicaQuiz.Model
{
    using System;

    public class QuizResult
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public TimeSpan TimeSpent { get; set; }

        public int AnswerId { get; set; }

        public virtual Answer Answer { get; set; }

        public int QuizEnrolmentId { get; set; }

        public virtual QuizEnrolment QuizEnrolment { get; set; }
    }
}
