using CarShopOnline_v3.Models.Interfaces;
using Microsoft.JSInterop;

namespace CarShopOnline_v3.Models.CarModel
{
    public class Car : ICar
    {
        public Guid CarId { get; set; }
        public string Mark { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Region { get; set; } = null!;
        public int Year { get; set; }
        public string EngineVolume { get; set; } = null!;
        public int HorsePower { get; set; }
        public string FuelType { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string Photo { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Price { get; set; }
        public string Contact { get; set; } = null!;

    }
}
