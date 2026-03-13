namespace CourtApp.Web.Logger
{
    public interface ILogger
    {
        void LogInformation(string message);
        void LogWarning(string message);
    }
}
