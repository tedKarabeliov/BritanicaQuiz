namespace BritanicaQuiz.Model
{
    using System;
    using System.Collections.Generic;

    public class QuestionSet
    {
        public QuestionSet()
        {
            this.Questions = new HashSet<Question>();
        }

        public int Id { get; set; }

        public string Text { get; set; }

        public SkillType SkillType { get; set; }

        public TimeSpan AnswerTime { get; set; }

        public int QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
