using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestSite.Models;

namespace TestSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

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

        public ViewResult GetMedicines()
        {
            Medicine medicine = new("A1", "A1", 1);
            Medicine medicine2 = new("A2", "A2", 2);
            Medicine medicine3 = new("A3", "A3", 3);

            List<Medicine> list = new List<Medicine>
            {
                medicine, medicine2, medicine3
            };

            return View(list);
        }

        [HttpPost]
        public IActionResult add(Medicine medicine)
        {
            if (ModelState.IsValid)
            {
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
