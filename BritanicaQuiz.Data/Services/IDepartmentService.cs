namespace BritanicaQuiz.Data.Services
{
    using System.Collections.Generic;
    using BritanicaQuiz.Model;

    public interface IDepartmentService
    {
        IList<Department> GetAllDepartments();
    }
}
