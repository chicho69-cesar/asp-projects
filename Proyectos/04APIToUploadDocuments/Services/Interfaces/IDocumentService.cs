using _04APIToUploadDocuments.Models;

namespace _04APIToUploadDocuments.Services.Interfaces {
    public interface IDocumentService {
        Task<bool> Upload(DocumentAPI document);
    }
}