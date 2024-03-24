using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestSite.Models;
using TestSite.Utils;

namespace TestSite.Controllers
{
    public class HomeController : Controller
    {
        APIManager APIManager = new();
        public static ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddMedicine()
        {
            return View();
        }

        public async Task<ViewResult> GetMedicines()
        {
            return View(await APIManager.getMedicines());
        }

        [HttpPost]
        public async Task<IActionResult> add(Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                medicine.ConvertImageToBase64String();

                _logger.LogInformation(medicine.Name);
                _logger.LogInformation(medicine.Storage);
                _logger.LogInformation(medicine.Count.ToString());
                _logger.LogInformation(medicine.ID.ToString());
                _logger.LogInformation(medicine.Photo);

                await APIManager.createMedicine(medicine);
                return Redirect("/");
            }
            return View("AddMedicine");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
