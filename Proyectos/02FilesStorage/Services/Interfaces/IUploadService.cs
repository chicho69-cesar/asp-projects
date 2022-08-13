using _02FilesStorage.Models;
using _02FilesStorage.Models.ViewModels;

namespace _02FilesStorage.Services.Interfaces {
    public interface IUploadService {
        Task<IEnumerable<IndexViewModel>> GetFilesAsync();
        Task<bool> UploadAsync(FileServer file);
    }
}