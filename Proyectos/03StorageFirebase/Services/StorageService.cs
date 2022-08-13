using _03StorageFirebase.Models;
using _03StorageFirebase.Services.Interfaces;
using Firebase.Auth;
using Firebase.Storage;
using System.Data;
using System.Data.SqlClient;

namespace _03StorageFirebase.Services {
    public class StorageService : IStorageService {
        private readonly string _email, _password, _route, _apiKey;
        private readonly string _connectionString;

        public StorageService(IConfiguration configuration) {
            _email = configuration["Firebase:email"];
            _password = configuration["Firebase:password"];
            _route = configuration["Firebase:route"];
            _apiKey = configuration["Firebase:apiKey"];

            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<FirebaseEmployee>> GetEmployeesAsync() {
            var employees = new List<FirebaseEmployee>();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var cmd = new SqlCommand("employees_list", connection) {
                CommandType = CommandType.StoredProcedure
            };

            using var dr = await cmd.ExecuteReaderAsync();

            while(await dr.ReadAsync()) {
                employees.Add(new FirebaseEmployee {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr["Name"].ToString(),
                    Phone = dr["Phone"].ToString(),
                    URLImage = dr["URLImage"].ToString()
                });
            }

            return employees;
        }
        
        public async Task<string> UploadStorageAsync(Stream file, string name) {
            var auth = new FirebaseAuthProvider(
                new FirebaseConfig(_apiKey)
            );
            var authenticate = await auth.SignInWithEmailAndPasswordAsync(_email, _password);
            var cancellationToken = new CancellationTokenSource();

            var task = new FirebaseStorage(
                _route,
                new FirebaseStorageOptions {
                    AuthTokenAsyncFactory = () => Task.FromResult(authenticate.FirebaseToken),
                    ThrowOnCancel = true
                }
            )
                .Child("profile_photos")
                .Child(name)
                .PutAsync(file, cancellationToken.Token);

            var downloadUrl = await task;

            return downloadUrl;
        }

        public async Task<bool> AddUserAsync(CreateViewModel employee) {
            try {
                using Stream image = employee.Image.OpenReadStream();
                string urlImage = await UploadStorageAsync(image, employee.Image.FileName);

                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                var cmd = new SqlCommand("save_employee", connection);
                cmd.Parameters.AddWithValue("Name", employee.Name);
                cmd.Parameters.AddWithValue("Phone", employee.Phone);
                cmd.Parameters.AddWithValue("URLImage", urlImage);
                cmd.CommandType = CommandType.StoredProcedure;

                await cmd.ExecuteNonQueryAsync();

                return true;
            } catch (Exception) {
                return false;
            }
        }
    }
}