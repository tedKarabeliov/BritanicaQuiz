namespace BritanicaQuiz.Data
{
    using System.Configuration;
    using System.Net;
    using System.Linq;
    using System.Data;
    using BritanicaOnlineExamRegistrationService;

    public class CRMWebServiceManager : ICRMWebServiceManager
    {
        private BritanicaOnlineExamRegistrationSoap webService;

        public CRMWebServiceManager(BritanicaOnlineExamRegistrationSoapClient webService)
        {
            this.webService = webService;
        }

        public DataTable GetTrainingGroups(string level)
        {
            var trainingGroups = this.webService.GetTrainingGroupsOnlineTests(new GetTrainingGroupsOnlineTestsRequest(null, level)).GetTrainingGroupsOnlineTestsResult.Tables[0];

            return trainingGroups;
        }
    }
}