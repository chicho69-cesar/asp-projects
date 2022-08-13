using _03StorageFirebase.Models;

namespace _03StorageFirebase.Services.Interfaces {
    public interface IStorageService {
        Task<bool> AddUserAsync(CreateViewModel employee);
        Task<IEnumerable<FirebaseEmployee>> GetEmployeesAsync();
        Task<string> UploadStorageAsync(Stream file, string name);
    }
}