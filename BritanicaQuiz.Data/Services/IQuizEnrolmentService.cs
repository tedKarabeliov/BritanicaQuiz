namespace BritanicaQuiz.Data.Services
{
    using BritanicaQuiz.Model;

    public interface IQuizEnrolmentService
    {
        QuizEnrolment GetEnrolment(int enrolmentId);

        void AddPoints(QuizEnrolment enrolment, int points);

        void RemovePoints(QuizEnrolment enrolment, int points);

        void ChangeQuizId(int enrolmentId, int quizId);

        void ResetPoints(int enrolmentId);

        void CreateEnrolment(CourseType courseType, string pastEnglishStudyingDescription, string englishGoalsDescription,
            int departmentId, int cityId, int hasCertificate, string certificateDescription, int certificateGrade, int quizId, string userId);

        QuizEnrolment GetActiveEnrolmentForUser(string user);

        QuizEnrolment GetLastEnrolmentForUser(string userId);

        void CompleteEnrolment(int enrolmentId);
    }
}
