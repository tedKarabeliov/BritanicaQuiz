namespace BritanicaQuiz.Data.Repositories
{
    using System.Collections.Generic;
    using System.Data;

    using BritanicaQuiz.Data;
    using BritanicaQuiz.Model;

    public class TrainingGroupsRepository
    {
        private ICRMWebServiceManager crmWebServiceManager;

        public TrainingGroupsRepository(ICRMWebServiceManager crmWebServiceManager)
        {
            this.crmWebServiceManager = crmWebServiceManager;
        }

        public IEnumerable<TrainingGroup> All(string level)
        {
            var groups = this.crmWebServiceManager.GetTrainingGroups(level);

            foreach (DataRow row in groups.Rows)
            {
                yield return new TrainingGroup()
                {
                    GroupId = row["Group_ID"].ToString(),
                    TrainingProduct = row["TrainingProduct"].ToString(),
                    StartingDate = row["StartingDate"].ToString(),
                    CourseTax = row["CourseTax"].ToString()
                };
            }
        }
    }
}
