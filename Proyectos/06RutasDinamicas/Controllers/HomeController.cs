using _06RutasDinamicas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _06RutasDinamicas.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult Privacy() {
            return View();
        }

        [HttpGet]
        public IActionResult Profile(string profileUser) {
            var users = new List<string> {
                "cesar", "liz", "lucy", "hector", "thaily"
            };
            
            if (!users.Contains(profileUser)) {
                return NotFound();
            }

            return View("Profile", profileUser);
        }

        [HttpGet]
        public IActionResult NotFoundPage() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode) {
            if (statusCode == 404) {
                return View(nameof(NotFoundPage));
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}