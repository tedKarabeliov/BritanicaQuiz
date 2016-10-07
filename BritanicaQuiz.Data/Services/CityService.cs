namespace BritanicaQuiz.Data.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using BritanicaQuiz.Data.Repositories;
    using BritanicaQuiz.Model;

    public class CityService : ICityService
    {
        private GenericRepository<City> cityRepository;

        public CityService(GenericRepository<City> cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        public IList<City> GetAllCities()
        {
            return this.cityRepository.All().ToList();
        }
    }
}
