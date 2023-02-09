using System.Diagnostics;
using CarShopOnline_v3.Models;
using CarShopOnline_v3.Models.CarModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarShopOnline_v3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            this._hostEnvironment = hostEnvironment;
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

        [Authorize]
        public IActionResult AddCar()
        {
            
            return View();
        }

        public async Task<IActionResult> AddCarToDatabaseAsync(
            string Model,
            string Mark,
            string Region,
            string Year,
            string EngineVolume,
            string HorsePower,
            string FuelType,
            string Body,
            string Description,
            string Price,
            string Photo,
            IFormFile ImageFile)
        {
            Car car = new Car()
            {
                Model = Model,
                Mark = Mark,
                Region = Region,
                Year = Int32.Parse(Year),
                EngineVolume = EngineVolume,
                HorsePower = Int32.Parse(HorsePower),
                FuelType = FuelType,
                Body = Body,
                Contact = User.Identity!.Name!,
                Description = Description,
                Price = Int32.Parse(Price),
                ImageFile = ImageFile
            };
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
            string extension = Path.GetExtension(ImageFile.FileName);
            car.Photo=fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = wwwRootPath + "/images/" + fileName;
            using (var fileStream = new FileStream(path,FileMode.Create))
            {
                await car.ImageFile.CopyToAsync(fileStream);
            }
            var dbContext = new CarShopDbContext();

            await dbContext.AddCarAsync(car);
            return View("AddCar");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}