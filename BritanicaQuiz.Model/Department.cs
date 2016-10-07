namespace BritanicaQuiz.Model
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("dbo.V_Departments")]
    public class Department
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
