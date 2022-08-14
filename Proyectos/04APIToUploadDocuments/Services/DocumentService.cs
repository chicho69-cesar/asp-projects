using _04APIToUploadDocuments.Models;
using _04APIToUploadDocuments.Services.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace _04APIToUploadDocuments.Services {
    public class DocumentService : IDocumentService {
        private readonly string _connectionString;
        private readonly string _serverRoute;

        public DocumentService(IConfiguration configuration) {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _serverRoute = configuration["Configuration:ServerRoute"];
        }

        public async Task<bool> Upload(DocumentAPI document) {
            string documentRoute = Path.Combine(_serverRoute, document.DocumentFile.FileName);

            try {
                using FileStream newFile = File.Create(documentRoute);
                await document.DocumentFile.CopyToAsync(newFile);
                await newFile.FlushAsync();

                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                var cmd = new SqlCommand("save_document", connection);
                cmd.Parameters.AddWithValue("Description", document.Description);
                cmd.Parameters.AddWithValue("Route", documentRoute);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();

                return true;
            } catch (Exception) {
                return false;
            }
        }
    }
}