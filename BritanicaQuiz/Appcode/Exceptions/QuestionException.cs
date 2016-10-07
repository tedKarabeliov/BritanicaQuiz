using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BritanicaQuiz.Appcode.Exceptions
{
    public class QuestionException : Exception
    {
        public QuestionException(string message)
            : base(message)
        {
        }
    }
}