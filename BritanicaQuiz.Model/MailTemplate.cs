namespace BritanicaQuiz.Model
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("dbo.V_Mail_Templates")]
    public class MailTemplate
    {
        public int Id { get; set; }

        public string MailSubject { get; set; }

        public string MailTemplateText { get; set; }
    }
}
