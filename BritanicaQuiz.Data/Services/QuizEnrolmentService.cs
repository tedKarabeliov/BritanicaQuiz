namespace BritanicaQuiz.Data.Services
{
    using BritanicaQuiz.Data.Exceptions;
    using BritanicaQuiz.Data.Repositories;
    using BritanicaQuiz.Model;
    using System;
    using System.Linq;

    public class QuizEnrolmentService : IQuizEnrolmentService
    {
        private GenericRepository<QuizEnrolment> quizEnrolmentRepository;

        public QuizEnrolmentService(GenericRepository<QuizEnrolment> quizEnrolmentRepository)
        {
            this.quizEnrolmentRepository = quizEnrolmentRepository;
        }

        public QuizEnrolment GetEnrolment(int enrolmentId)
        {
            var enrolment = quizEnrolmentRepository.All().FirstOrDefault(qe => qe.Id == enrolmentId);

            return enrolment;
        }

        public void AddPoints(QuizEnrolment enrolment, int points)
        {
            enrolment.TotalPoints += points;

            this.quizEnrolmentRepository.SaveChanges();
        }

        public void RemovePoints(QuizEnrolment enrolment, int points)
        {
            enrolment.TotalPoints -= points;

            this.quizEnrolmentRepository.SaveChanges();
        }

        public void ChangeQuizId(int enrolmentId, int quizId)
        {
            var enrolment = this.GetEnrolment(enrolmentId);

            enrolment.QuizId = quizId;

            this.quizEnrolmentRepository.SaveChanges();
        }

        public void ResetPoints(int enrolmentId)
        {
            var enrolment = this.GetEnrolment(enrolmentId);

            enrolment.TotalPoints = 0;

            this.quizEnrolmentRepository.SaveChanges();

            this.quizEnrolmentRepository.SaveChanges();
        }

        public void CreateEnrolment(CourseType courseType, string pastEnglishStudyingDescription, string englishGoalsDescription,
            int departmentId, int cityId, int hasCertificate, string certificateDescription,
            int certificateGrade, int quizId, string userId)
        {
            var enrolment = new QuizEnrolment()
            {
                CourseType = courseType,
                PastEnglishStudyingDescription = pastEnglishStudyingDescription,
                EnglishGoalsDescription = englishGoalsDescription,
                DepartmentId = departmentId,
                CityId = cityId,
                CambridgeCertificate = hasCertificate,
                CambridgeCertificateDescription = certificateDescription,
                CambridgeCertificateGrade = certificateGrade,
                TotalPoints = 0,
                IsCompleted = false,
                OralResult = 0,
                OralResultTeacherComment = null,
                Status = QuizEnrolmentStatus.New,
                DateCreated = DateTime.Now,
                UserId = userId,
                TeacherId = null,
                QuizId = quizId,
                ChosenTeacherQuizId = quizId
            };

            this.quizEnrolmentRepository.Add(enrolment);
            this.quizEnrolmentRepository.SaveChanges();
        }

        public QuizEnrolment GetActiveEnrolmentForUser(string userId)
        {
            var count = this.quizEnrolmentRepository.All().Count(
                 qr => qr.UserId == userId && qr.IsCompleted == false);

            if (count > 1)
            {
                throw new DatabaseIncompatibilityException("There is more than 1 enrolment active for user");
            }

            var enrolment = this.quizEnrolmentRepository.All().FirstOrDefault(
                qr => qr.UserId == userId && qr.IsCompleted == false);

            return enrolment;
        }

        public void CompleteEnrolment(int enrolmentId)
        {
            var enrolment = this.GetEnrolment(enrolmentId);

            enrolment.IsCompleted = true;
            enrolment.Status = QuizEnrolmentStatus.ForOralInterview;

            this.quizEnrolmentRepository.SaveChanges();
        }

        public QuizEnrolment GetLastEnrolmentForUser(string userId)
        {
            var enrolment = this.quizEnrolmentRepository.All()
                .Where(qr => qr.UserId == userId && qr.IsCompleted == true)
                .OrderByDescending(qr => qr.DateCreated).FirstOrDefault();

            return enrolment;
        }
    }
}
