namespace BritanicaQuiz.Appcode
{
    using BritanicaQuiz.Appcode.CalculatePoints;
    using BritanicaQuiz.Data.Services;
    using BritanicaQuiz.Model;
    using Ninject;
    using System;
    using System.Collections.Generic;

    public interface IQuestionManager
    {
        IList<QuestionSet> ShuffleQuestionSets(IList<QuestionSet> questionSets, Random random);

        void AnswerQuestion(Question question, int enrolmentId, IList<int> answerIds, IList<string> answerTexts, TimeSpan timeLeft);

        void CalculatePointsForQuestion(Question question, QuizEnrolment quizEnrolment, ICalculateIsCorrectStrategy calculateIsCorrectStrategy);

        ICalculateIsCorrectStrategy GetCalculatePointsOption(Question question);

        bool IsAnswered(int enrolmentId, QuestionSet questionSet);

        List<int> ExtractAnswerIdsForQuestion(Question question, IList<int> answerIds);

        List<string> ExtractAnswerTextsForQuestion(Question question, IList<string> answerTexts, IList<int> textAnswerIds);
    }
}