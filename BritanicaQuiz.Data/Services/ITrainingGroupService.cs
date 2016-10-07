namespace BritanicaQuiz.Data.Services
{
    using System.Collections.Generic;

    using BritanicaQuiz.Model;

    public interface ITrainingGroupService
    {
        IList<TrainingGroup> GetAllTrainingGroups(string level);
    }
}
