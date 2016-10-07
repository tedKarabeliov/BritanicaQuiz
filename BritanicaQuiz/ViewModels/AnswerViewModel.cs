namespace BritanicaQuiz.ViewModels
{
    using BritanicaQuiz.Model;
    using System.Collections.Generic;

    public class AnswerViewModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Template { get; set; }

        public int QuestionId { get; set; }

        public IList<QuizResult> QuizResults { get; set; }
    }
}