namespace BritanicaQuiz.Appcode
{
    using System.Collections.Generic;

    using BritanicaQuiz.Model;

    public interface IQuestionSetCollection
    {
        int Index { get; }

        int Count { get; }

        bool Full { get; }

        void AssignQuestionSets(IList<QuestionSet> questions);

        QuestionSet GetNextQuestionSet();

        QuestionSet GetCurrentQuestionSet();

        QuestionSet GetPreviousQuestionSet();

        void Reset();
    }
}