using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BritanicaQuiz.ViewModels
{
    public class QuizResultInputViewModel
    {
        public IList<int> TextAnswerIds { get; set; }

        public IList<int> Answer { get; set; }

        public IList<string> AnswerText { get; set; }

        public int Hours { get; set; }

        public int Minutes { get; set; }

        public int Seconds { get; set; }
    }
}