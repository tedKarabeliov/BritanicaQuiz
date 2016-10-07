namespace BritanicaQuiz.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System;

    public abstract class Question
    {
        public Question()
        {
            this.Answers = new HashSet<Answer>();
        }

        public int Id { get; set; }

        public string Text { get; set; }

        public int QuestionSetId { get; set; }

        public virtual QuestionSet QuestionSet { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        [NotMapped]
        public abstract string HtmlPartial { get; }
    }
}
