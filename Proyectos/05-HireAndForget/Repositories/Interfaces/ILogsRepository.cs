namespace _05_HireAndForget.Repositories.Interfaces {
    public interface ILogsRepository {
        Task BackgroundSaveLogs(string message);
    }
}