using _01Autocomplete.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;

namespace _01Autocomplete.Controllers {
    public class HomeController : Controller {
        private readonly string _connectionString;

        public HomeController(IConfiguration configuration) {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public JsonResult SearchProducts(string searched) {
            List<Search> searchList = new List<Search>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("sp_search_products", connection);
            command.Parameters.AddWithValue("search", searched);
            command.CommandType = CommandType.StoredProcedure;

            using var results = command.ExecuteReader();

            while (results.Read()) {
                searchList.Add(new Search {
                    value = Convert.ToInt32(results["id"]),
                    label = results["Text"].ToString(),
                    price = Convert.ToDecimal(results["price"]),
                    description = results["description"].ToString()
                });
            }

            return Json(searchList);
        }

        [HttpGet]
        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}