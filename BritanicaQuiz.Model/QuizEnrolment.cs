namespace BritanicaQuiz.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class QuizEnrolment
    {
        public QuizEnrolment()
        {
            this.QuizResults = new HashSet<QuizResult>();
        }

        public int Id { get; set; }

        public CourseType CourseType { get; set; }

        public string PastEnglishStudyingDescription { get; set; }

        public string EnglishGoalsDescription { get; set; }

        public int DepartmentId { get; set; }

        public int CityId { get; set; }

        public int CambridgeCertificate { get; set; }

        public string CambridgeCertificateDescription { get; set; }

        public int CambridgeCertificateGrade { get; set; }

        public int TotalPoints { get; set; }

        public int TotalPointsTeacher { get; set; }

        public bool IsCompleted { get; set; }

        public QuizEnrolmentStatus Status { get; set; }

        public int OralResult { get; set; }

        public string OralResultTeacherComment { get; set; }

        public DateTime DateCreated { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public string TeacherId { get; set; }

        public virtual User Teacher { get; set; }

        public int QuizId { get; set; }

        [ForeignKey("Quiz")]
        public virtual Quiz Quiz { get; set; }

        [ForeignKey("Quiz")]
        public int ChosenTeacherQuizId { get; set; }

        public virtual Quiz ChosenTeacherQuiz { get; set; }

        public virtual ICollection<QuizResult> QuizResults { get; set; }
    }
}
