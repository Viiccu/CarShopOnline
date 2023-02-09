namespace CarShopOnline_v3.Models.Interfaces
{
    public interface ICar
    {
        public Guid CarId { get; set; }
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
        public IFormFile ImageFile { get; set; }
    }
}
