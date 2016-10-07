namespace BritanicaQuiz.Data.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using BritanicaQuiz.Model;
    using BritanicaQuiz.Data.Repositories;

    public class DepartmentService : IDepartmentService
    {
        private GenericRepository<Department> departmentRepository;

        public DepartmentService(GenericRepository<Department> departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        public IList<Department> GetAllDepartments()
        {
            return this.departmentRepository.All().ToList();
        }
    }
}
