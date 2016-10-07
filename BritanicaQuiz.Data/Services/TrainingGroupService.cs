namespace BritanicaQuiz.Data.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using BritanicaQuiz.Model;
    using BritanicaQuiz.Data.Repositories;

    public class TrainingGroupService : ITrainingGroupService
    {
        private TrainingGroupsRepository trainingGroupsRepository;

        public TrainingGroupService(TrainingGroupsRepository trainingGroupsRepository)
        {
            this.trainingGroupsRepository = trainingGroupsRepository;
        }

        public IList<TrainingGroup> GetAllTrainingGroups(string level)
        {
            return this.trainingGroupsRepository.All(level).ToList();
        }
    }
}
