using BritanicaQuiz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BritanicaQuiz.ViewModels
{
    public class QuestionSetViewModel
    {
        public string Text { get; set; }

        public TimeSpan AnswerTime { get; set; }

        public IList<QuestionViewModel> Questions { get; set; }
    }
}