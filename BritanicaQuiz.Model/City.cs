namespace BritanicaQuiz.Model
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("dbo.V_Cities")]
    public class City
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
