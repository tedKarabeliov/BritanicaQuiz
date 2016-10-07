using BritanicaQuiz.Model;
using System;
using System.Collections.Generic;
namespace BritanicaQuiz.ViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public IList<AnswerViewModel> Answers { get; set; }

        public string HtmlPartial { get; set; }
    }
}