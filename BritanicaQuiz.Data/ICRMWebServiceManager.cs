namespace BritanicaQuiz.Data
{
    using System.Data;

    public interface ICRMWebServiceManager
    {
        DataTable GetTrainingGroups(string level);
    }
}
