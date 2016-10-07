namespace BritanicaQuiz.ViewModels
{
    using BritanicaQuiz.Model;

    public class QuizEnrolmentInputViewModel
    {
        public int QuizId { get; set; }

        public string PastEnglishStudyingDescription { get; set; }

        public string EnglishGoalsDescription { get; set; }

        public int CityId { get; set; }

        public int hasCertificate { get; set; }

        public string CertificateDescription { get; set; }

        public int CertificateGrade { get; set; }

        public string CertificateTime { get; set; }

        public int DepartmentId { get; set; }

        public CourseType CourseType { get; set; }
    }
}
