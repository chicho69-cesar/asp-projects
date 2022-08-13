using _02FilesStorage.Models;
using _02FilesStorage.Models.ViewModels;
using _02FilesStorage.Services.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace _02FilesStorage.Services {
    public class UploadService : IUploadService {
        private readonly string _connectionString;
        
        public UploadService(IConfiguration configuration) {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> UploadAsync(FileServer file) {
            try {
                using var ms = new MemoryStream();
                await file.FileDB.CopyToAsync(ms);

                byte[] data = ms.ToArray();

                using var connection = new SqlConnection(_connectionString);
                
                SqlCommand cmd = new(
                    @"INSERT INTO ServerFiles(Name, FileDB, Extension)
                    VALUES(@name, @file, @extension);",
                    connection
                );

                cmd.Parameters.AddWithValue("@name", file.Name);
                cmd.Parameters.AddWithValue("@file", data);
                cmd.Parameters.AddWithValue("@extension", file.Extension);
                cmd.CommandType = CommandType.Text;

                connection.Open();
                cmd.ExecuteNonQuery();

                return true;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<IEnumerable<IndexViewModel>> GetFilesAsync() {
            var fileList = new List<IndexViewModel>();

            using var connection = new SqlConnection(_connectionString);

            SqlCommand cmd = new(
                @"SELECT * FROM ServerFiles;",
                connection
            );

            cmd.CommandType = CommandType.Text;
            connection.Open();

            using var dr = await cmd.ExecuteReaderAsync();
            
            while (await dr.ReadAsync()) {
                var item = new IndexViewModel {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr["Name"].ToString(),
                    FileDB = dr["FileDB"] as byte[],
                    Extension = dr["Extension"].ToString()
                };

                fileList.Add(item);
            }

            return fileList;
        }
    }
}