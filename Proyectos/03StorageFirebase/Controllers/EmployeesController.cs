using _03StorageFirebase.Models;
using _03StorageFirebase.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace _03StorageFirebase.Controllers {
    public class EmployeesController : Controller {
        private readonly IStorageService _storageService;

        public EmployeesController(IStorageService storageService) {
            _storageService = storageService;
        }

        [HttpGet]
        public async Task<IActionResult> Index() {
            var employees = await _storageService.GetEmployeesAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model) {
            if (!ModelState.IsValid) {
                ModelState.AddModelError(string.Empty, "Ocurrio un error con la informacion proporcionada");
                return View(model);
            }

            var employeeWasAdded = await _storageService.AddUserAsync(model);

            if (employeeWasAdded) {
                return RedirectToAction(nameof(Index));
            } else {
                ModelState.AddModelError(string.Empty, "Ocurrio un error al agregar al usuario pana");
                return View(model);
            }
        }
    }
}