namespace BritanicaQuiz.Appcode
{
    using Ninject;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using BritanicaQuiz.Appcode.CalculatePoints;
    using BritanicaQuiz.Appcode.Exceptions;
    using BritanicaQuiz.Model;
    using BritanicaQuiz.Appcode.Common;
    using BritanicaQuiz.Data.Services;

    public class QuestionManager : IQuestionManager
    {
        private IQuizResultService quizResultService;
        private IQuizEnrolmentService quizEnrolmentService;
        private IAnswerService answerService;
        private IQuestionSetService questionSetService;
        private int index;

        public QuestionManager(IQuizResultService quizResultService,
            IQuizEnrolmentService quizEnrolmentService, IAnswerService answerService, IQuestionSetService questionSetService)
        {
            this.quizResultService = quizResultService;
            this.quizEnrolmentService = quizEnrolmentService;
            this.answerService = answerService;
            this.questionSetService = questionSetService;
        }

        public IList<QuestionSet> ShuffleQuestionSets(IList<QuestionSet> questionSets, Random random)
        {
            var mathOperations = new MathOperations();

            mathOperations.Shuffle<QuestionSet>(questionSets, random);

            questionSets = questionSets.OrderBy(q => q.SkillType).ToList();

            return questionSets;
        }

        public void AnswerQuestion(Question question, int enrolmentId, IList<int> answerIds, IList<string> answerTexts, TimeSpan timeleft)
        {
            if (quizResultService.QuizResultExists(question.Id, enrolmentId))
            {
                quizResultService.DeleteQuizResultsForQuestion(question.Id, enrolmentId);
            }

            var timeSpent = this.CalculateTimeSpentOnAnsweringQuestionSet(question.QuestionSet.AnswerTime, timeleft);

            if (answerIds == null && (answerTexts == null || answerTexts.All(a => a == string.Empty)))
            {
                var emptyAnswer = this.answerService.GetEmptyAnswer(question.Id);

                quizResultService.AddQuizResult(emptyAnswer.Id, enrolmentId, null, timeSpent);
            }
            else
            {
                this.AddQuizResult(question, answerIds, answerTexts, timeSpent);
            }
        }

        public void CalculatePointsForQuestion(Question question, QuizEnrolment quizEnrolment,
            ICalculateIsCorrectStrategy calculateIsCorrectStrategy)
        {
            var answers = question.Answers.ToList();

            foreach (var answer in answers)
            {
                var quizResult = answer.QuizResults.FirstOrDefault(qr => qr.QuizEnrolment == quizEnrolment);

                if (answer.Text != this.answerService.EmptyAnswerText)
                {
                    var isCorrect = calculateIsCorrectStrategy.CalculateIsCorrectQuestion(answer, quizResult);

                    if (isCorrect)
                    {
                        quizEnrolmentService.AddPoints(quizEnrolment, answer.Points);
                    }
                    else
                    {
                        if (answer.NegativePoints != 0 && !(answer.IsCorrect) && quizResult != null)
                        {
                            quizEnrolmentService.RemovePoints(quizEnrolment, answer.NegativePoints);
                        }
                    }
                }
            }
        }

        public ICalculateIsCorrectStrategy GetCalculatePointsOption(Question question)
        {
            ICalculateIsCorrectStrategy calculatePointsStrategy = null;

            var questionType = question.GetType().BaseType.Name;

            switch (question.GetType().BaseType.Name)
            {
                case "InputAnswerQuestion":
                    calculatePointsStrategy = new InputAnswerQuestionCalculatePointsStrategy();
                    break;
                case "MultipleAnswersQuestion":
                    calculatePointsStrategy = new MultipleAnswersQuestionCalculatePointsStrategy();
                    break;
                case "OneAnswerQuestion":
                    calculatePointsStrategy = new OneAnswerQuestionCalculatePointsStrategy();
                    break;
                case "OpenTextQuestion":
                    calculatePointsStrategy = new OpenTextQuestionCalculatePointsStrategy();
                    break;
                case "ReadingQuestion":
                    calculatePointsStrategy = new ReadingQuestionCalculatePointsStrategy();
                    break;
                default:
                    throw new QuestionException("No strategy found for answering question of type - " + questionType);
            }

            return calculatePointsStrategy;
        }

        private void AddQuizResult(Question question, IList<int> answerIds, IList<string> answerTexts, TimeSpan timeSpent)
        {
            var enrolmentId = int.Parse(HttpContext.Current.Session["currentQuizEnrolmentId"].ToString());

            var questionAnswers = question.Answers.ToList();

            if (!(answerIds == null && answerTexts == null))
            {
                var resultCount = answerIds == null ? answerTexts.Count : answerIds.Count;

                for (int i = 0; i < resultCount; i++)
                {
                    var answerIdFromResult = answerIds == null ? questionAnswers[i].Id : answerIds[i];
                    var textFromResult = answerTexts == null ? null : answerTexts[i];

                    if (textFromResult != string.Empty)
                    {
                        quizResultService.AddQuizResult(answerIdFromResult, int.Parse(HttpContext.Current.Session["currentQuizEnrolmentId"].ToString()), textFromResult, timeSpent);
                    }
                }
            }
        }

        private TimeSpan CalculateTimeSpentOnAnsweringQuestionSet(TimeSpan answerTotalTime, TimeSpan timeLeft)
        {
            return answerTotalTime - timeLeft;
        }

        public bool IsAnswered(int enrolmentId, QuestionSet questionSet)
        {
            foreach (var question in questionSet.Questions)
            {
                if (quizResultService.QuizResultExists(question.Id, enrolmentId))
                {
                    return true;
                }
            }

            return false;
        }


        public List<int> ExtractAnswerIdsForQuestion(Question question, IList<int> answerIds)
        {
            List<int> answerIdsToReturn = null;

            foreach (var answerId in answerIds)
            {
                if (question.Answers.Any(a => a.Id == answerId))
                {
                    if (answerIdsToReturn == null)
                    {
                        answerIdsToReturn = new List<int>();
                    }

                    answerIdsToReturn.Add(answerId);
                }
            }

            return answerIdsToReturn;
        }

        public List<string> ExtractAnswerTextsForQuestion(Question question, IList<string> answerTexts, IList<int> textAnswerIds)
        {
            List<string> answerTextsToReturn = null;

            for (int i = 0; i < answerTexts.Count; i++)
            {
                if (question.Answers.Any(a => a.Id == textAnswerIds[i]))
                {
                    if (answerTextsToReturn == null)
                    {
                        answerTextsToReturn = new List<string>();
                    }

                    answerTextsToReturn.Add(answerTexts[i]);
                }
            }

            return answerTextsToReturn;
        }
    }
}