using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarShopOnline_v3.Views.Home
{
    public class CarDetails : PageModel
    {
        public IFormFile ImageFile { get; set; }
        public int CarId { get; set; }
    }
}
