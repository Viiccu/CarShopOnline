using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarShopOnline_v3.Views.Home
{
    public class CarDetails : PageModel
    {
        public int ImageNumber { get; set; } = 0;
        public string Image { get; set; }
        public FormFile ImageFile { get; set; }
        public int CarId { get; set; }

        public int IncreaseImageNumber()
        {
            ImageNumber++;
            return ImageNumber;
        }

        public void DecreaseImageNumber()
        {
            ImageNumber--;
        }
    }
}
