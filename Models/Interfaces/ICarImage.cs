namespace CarShopOnline_v3.Models.Interfaces
{
    public interface ICarImage
    {
        public Guid CarId { get; set; }
        public string Image { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
