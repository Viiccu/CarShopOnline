using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CarShopOnline_v3.Models.Interfaces;

namespace CarShopOnline_v3.Models.CarModel
{
    public class CarImage : ICarImage
    {
        public Guid CarId { get; set; }
        [Key]
        public string Image { get; set; }
        
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
