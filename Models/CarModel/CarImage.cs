using System.ComponentModel.DataAnnotations.Schema;
using CarShopOnline_v3.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarShopOnline_v3.Models.CarModel
{
    [Keyless]
    public class CarImage : ICarImage
    {
        public Guid CarId { get; set; }
        public string Image { get; set; }
        
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
