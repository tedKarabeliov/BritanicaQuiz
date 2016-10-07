namespace BritanicaQuiz.Data.Services
{
    using System.Collections.Generic;
    using BritanicaQuiz.Model;

    public interface ICityService
    {
        IList<City> GetAllCities();
    }
}
