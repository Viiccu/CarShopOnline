namespace CarShopOnline_v3.Models.Interfaces
{
    public interface ICar
    {
        public int CarId { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        public string Region { get; set; }
        public int Year { get; set; }
        public string EngineVolume { get; set; }
        public int HorsePower { get; set; }
        public string FuelType { get; set; }
        public string Body { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Contact { get; set; }

        public Task<List<ICar>> GetAllCarsAsync();
        public Task<ICar> GetCarByIdAsync(int carId);
        public Task<List<ICar>> GetCarByRegionAsync(string RegionName);
        public Task RemoveCarByIdAsync(int removeCarId);
        public Task UpdateCarByIdAsync(int updateCarId);
    }
}
