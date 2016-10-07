namespace BritanicaQuiz.Appcode
{
    using BritanicaQuiz.Appcode.CalculatePoints;
    using BritanicaQuiz.Appcode.Common;
    using BritanicaQuiz.Data.Services;
using BritanicaQuiz.Model;

    public class QuizManager : IQuizManager
    {
        private IQuestionService questionService;
        private IQuizEnrolmentService quizEnrolmentService;
        private IQuizService quizService;
        private IQuestionManager questionManager;
        private IQuestionSetService questionSetService;

        public QuizManager(IQuestionService questionService, IQuizEnrolmentService quizEnrolmentService,
            IQuizService quizService, IQuestionManager questionManager, IQuestionSetService questionSetService)
        {
            this.questionService = questionService;
            this.quizEnrolmentService = quizEnrolmentService;
            this.quizService = quizService;
            this.questionManager = questionManager;
            this.questionSetService = questionSetService;
        }

        public void CalculatePoints(int enrolmentId)
        {
            var enrolment = quizEnrolmentService.GetEnrolment(enrolmentId);

            var questionSets = this.questionSetService.GetAllQuestionSets(enrolment.QuizId);

            foreach (var questionSet in questionSets)
            {
                var questions = questionService.GetAllQuestions(questionSet.Id);

                foreach (var question in questions)
                {
                    var calculatePointsStrategy = this.questionManager.GetCalculatePointsOption(question);

                    this.questionManager.CalculatePointsForQuestion(question, enrolment, calculatePointsStrategy);
                }
            }
        }

        public int? GetNextQuiz(int enrolmentId, int percentageSuccess)
        {
            var enrolment = this.quizEnrolmentService.GetEnrolment(enrolmentId);

            if (percentageSuccess > 80)
            {
                return this.quizService.GetNextQuizId(enrolment.QuizId);
            }
            else if (percentageSuccess >= 60 && percentageSuccess <= 80)
            {
                return null;
            }
            else
            {
                return this.quizService.GetPreviousQuizId(enrolment.QuizId);
            }
        }
    }
}