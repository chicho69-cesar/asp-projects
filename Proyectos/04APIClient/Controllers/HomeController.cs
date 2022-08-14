using _04APIClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace _04APIClient.Controllers {
    public class HomeController : Controller {
        public HomeController() {}

        [HttpGet]
        public IActionResult Index() {
            ViewBag.Message = TempData["message"];
            return View();
        }

        [HttpGet]
        public IActionResult Privacy() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(string description, IFormFile file) {
            var client = new HttpClient();

            using var multipartFormContent = new MultipartFormDataContent();

            var fileStreamContent = new StreamContent(file.OpenReadStream());
            fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            
            multipartFormContent.Add(new StringContent(description), name: "Description");
            multipartFormContent.Add(fileStreamContent, name: "DocumentFile", fileName: file.FileName);

            var request = await client.PostAsync("https://localhost:7204/api/Documents/upload", multipartFormContent);
            var response = request.Content.ReadAsStringAsync();

            TempData["message"] = "Imagen guardada con exito !!!";

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}