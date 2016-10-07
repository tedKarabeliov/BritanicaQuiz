namespace BritanicaQuiz.Data.Exceptions
{
    using System;

    public class DatabaseIncompatibilityException : Exception
    {
        public DatabaseIncompatibilityException(string message)
            : base(message)
        {
        }
    }
}
