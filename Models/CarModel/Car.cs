using CarShopOnline_v3.Models.Interfaces;
using Microsoft.JSInterop;

namespace CarShopOnline_v3.Models.CarModel
{
    public class Car : ICar
    {
        public int CarId { get; set; }
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

        [JSInvokable]
        public async Task<List<ICar>> GetAllCarsAsync()
        {
            var cars = new List<ICar>()
            {
                new Car()
                {
                    Mark = "BMW",
                    Model = "M3",
                    Region = "Moldova",
                    Year = 2012,
                    Price = 14000
                },
                new Car()
                {
                    Mark = "Mercedes",
                    Model = "benz",
                    Region = "Moldova",
                    Year = 2005,
                    Price = 12000
                },
                new Car()
                {
                    Mark = "Volkswagen",
                    Model = "Passat",
                    Region = "Greece",
                    Year = 2005,
                    Price = 6200
                },
                new Car()
                {
                    Mark = "Mazda",
                    Model = "M3",
                    Region = "Greece",
                    Year = 2012,
                    Price = 16000
                },
                new Car()
                {
                    Mark = "Renault",
                    Model = "Grand Scenic 3",
                    Region = "Germany",
                    Year = 2014,
                    Price = 9700
                }
            };
            return await Task.FromResult(cars);
        }

        public Task<ICar> GetCarByIdAsync(int carId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ICar>> GetCarByRegionAsync(string RegionName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveCarByIdAsync(int removeCarId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCarByIdAsync(int updateCarId)
        {
            throw new NotImplementedException();
        }
    }
}
