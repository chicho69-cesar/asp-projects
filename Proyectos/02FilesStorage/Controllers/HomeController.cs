using _02FilesStorage.Models;
using _02FilesStorage.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _02FilesStorage.Controllers {
    public class HomeController : Controller {
        private readonly IUploadService _uploadService;

        public HomeController(IUploadService uploadService) {
            _uploadService = uploadService;
        }

        [HttpGet]
        public async Task<IActionResult> Index() {
            var fileList = await _uploadService.GetFilesAsync();
            return View(fileList);
        }

        [HttpGet]
        public IActionResult Upload() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(string name, IFormFile fileDB) {
            var model = new FileServer {
                Name = name,
                FileDB = fileDB,
                Extension = Path.GetExtension(fileDB.FileName)
            };

            var wasUploaded = await _uploadService.UploadAsync(model);
            
            if (wasUploaded) {
                return RedirectToAction(nameof(Index), "Home");
            } else {
                ModelState.AddModelError(String.Empty, "Ocurrio un error al subir el archivo");
                return View();
            }
        }

        [HttpPost]
        public async Task<FileResult> Download(int fileId) {
            var fileList = await _uploadService.GetFilesAsync();
            
            var file = fileList
                .Where(f => f.Id == fileId)
                .FirstOrDefault();

            string fullName = file.Name + file.Extension;

            return File(file.FileDB, "application/" + file.Extension.Replace(".", ""), fullName);
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