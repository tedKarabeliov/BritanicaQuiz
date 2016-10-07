using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BritanicaQuiz.Appcode.Exceptions
{
    public class QuizException : Exception
    {
        public QuizException(string message)
            : base(message)
        {
        }
    }
}