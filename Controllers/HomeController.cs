using System.Diagnostics;
using CarShopOnline_v3.Models;
using CarShopOnline_v3.Models.CarModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarShopOnline_v3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int currentPage = 1, int pageDimension = 12, string searchBoxText = "", string Region = "Moldova")
        {
            var dbContext = new CarShopDbContext(); 
            var cars = await dbContext.GetCarByRegionAsync(Region);

            ViewBag.Cars = cars.Skip((currentPage - 1) * pageDimension).Take(pageDimension).ToList();  
            ViewBag.CurrentPage = currentPage;
            ViewBag.SearchBoxText = searchBoxText;

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Identity.Application");
            await HttpContext.SignOutAsync("Identity.External");
            await HttpContext.SignOutAsync("Identity.TwoFactorRememberMe");
            await HttpContext.SignOutAsync("Identity.TwoFactorUserId");
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> CarDetails(Guid carId)
        {
            var dbContext = new CarShopDbContext();
            var images = await dbContext.GetCarImagesByIdAsync(new Guid("3bb1c1b5-5cdb-42ca-94d9-4dc81623228e"));
            ViewBag.CarImages = images.Select(x => x.Image).ToList();
            return View();
        }

        [Authorize]
        public IActionResult MyCars()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}