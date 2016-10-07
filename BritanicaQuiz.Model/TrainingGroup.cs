namespace BritanicaQuiz.Model
{
    using System.ComponentModel.DataAnnotations.Schema;

    [NotMapped]
    public class TrainingGroup
    {
        public string GroupId { get; set; }

        public string TrainingProduct { get; set; }

        public string StartingDate { get; set; }

        public string CourseTax { get; set; }
    }
}
