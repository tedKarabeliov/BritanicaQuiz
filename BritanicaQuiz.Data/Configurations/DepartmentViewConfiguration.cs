namespace BritanicaQuiz.Data.Configurations
{
    using System.Data.Entity.ModelConfiguration;
    using BritanicaQuiz.Model;

    public class DepartmentViewConfiguration : EntityTypeConfiguration<Department>
    {
        public DepartmentViewConfiguration()
        {
            //this.HasKey(d => d.Id);
            //this.ToTable("V_Departments");
        }
    }
}
