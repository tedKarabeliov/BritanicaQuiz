namespace BritanicaQuiz.Appcode.Exceptions
{
    using System;

    public class QuizEnrolmentException : Exception
    {
        public QuizEnrolmentException(string message)
            : base(message)
        {
        }
    }
}