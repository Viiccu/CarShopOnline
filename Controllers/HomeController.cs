using System.Diagnostics;
using CarShopOnline_v3.Models;
using CarShopOnline_v3.Models.CarModel;
using CarShopOnline_v3.Models.user;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace CarShopOnline_v3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly CarShopDbContext dbContext;
        private readonly UserManager<CarShopUser> userManager;
        private readonly SignInManager<CarShopUser> signInManager;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostEnvironment, UserManager<CarShopUser> userManager, SignInManager<CarShopUser> signInManager)
        {
            dbContext = new CarShopDbContext();
            this.logger = logger;
            this.hostEnvironment = hostEnvironment;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index(string Region, int currentPage = 1, int pageDimension = 12, string searchBoxText = "")
        {
            
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (String.IsNullOrEmpty(Region))
                Region = user.Region;

            var cars = await dbContext.GetCarByRegionAsync(Region);

            ViewBag.Cars = cars.Skip((currentPage - 1) * pageDimension).Take(pageDimension).ToList();  
            ViewBag.CurrentPage = currentPage;
            ViewBag.SearchBoxText = searchBoxText;
            ViewBag.Background = user.BgImage + ".jpg";
            
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

        [Authorize]
        [Route("Home/CarDetails/{carId:Guid}")]
        public async Task<IActionResult> CarDetails([FromRoute]Guid carId)
        {
            var images = await dbContext.GetCarImagesByIdAsync(carId);
            ViewBag.CarImages = images.Select(x => x.Image).ToList();
            Car car;
            ViewBag.Car = car = await dbContext.GetCarByIdAsync(carId);
            var user = await userManager.FindByEmailAsync(car.Contact);
            if(user != null)
                ViewBag.PhoneNumber = await userManager.GetPhoneNumberAsync(user);
            return View();
        }

        [Authorize]
        public async Task<IActionResult> MyCars()
        {
            ViewBag.Cars = await dbContext.GetAllCarsByOwnerAsync(User.Identity!.Name!);
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
            string wwwRootPath = hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
            string extension = Path.GetExtension(ImageFile.FileName);
            car.Photo=fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = wwwRootPath + "/images/" + fileName;
            using (var fileStream = new FileStream(path,FileMode.Create))
            {
                await car.ImageFile.CopyToAsync(fileStream);
            }

            await dbContext.AddCarAsync(car);
            return View("AddCar");
        }

        [HttpPost]
        public async Task<IActionResult> AddImage(IFormFile ImageFile, Guid carId)
        {
            string wwwRootPath = hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
            string extension = Path.GetExtension(ImageFile.FileName);
            fileName += DateTime.Now.ToString("yymmssfff") + extension;
            string path = wwwRootPath + "/images/" + fileName;
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await ImageFile.CopyToAsync(fileStream);
            }
            await dbContext.AddImageAsync(carId, fileName);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("CarDetails", new { carId = carId });
        }

        public async Task<IActionResult> RemoveImage(Guid carId, List<string> imagesToDelete)
        {
            ViewBag.CarId = carId;
            var images = await dbContext.GetCarImagesByIdAsync(carId);
            ViewBag.CarImages = images.Select(x => x.Image).ToList();
            if (imagesToDelete.Count > 0)
            {
                foreach (var image in imagesToDelete)
                {
                    DeleteSelectedImages(image);
                    //var filePath = $"~\\images\\{image}";
                    string filePath = hostEnvironment.WebRootPath + $"/images/{image}";
                    FileInfo file = new FileInfo(filePath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
                return RedirectToAction("RemoveImage", new { carId, imagesToDelete = new List<string>() });
            }
            return View("RemoveImage", new { carId, imagesToDelete = new List<string>() });
        }

        async void DeleteSelectedImages(string imageName)
        {
            await dbContext.RemoveImageAsync(imageName);
        }

        public async Task<IActionResult> UpdateCarDetails(Car car)
        {
            await dbContext.UpdateCarDetailsAsync(car);
            return RedirectToAction("CarDetails", new { car.CarId });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


