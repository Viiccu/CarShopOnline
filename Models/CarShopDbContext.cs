﻿using CarShopOnline_v3.Models.CarModel;
using CarShopOnline_v3.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarShopOnline_v3.Models
{
    public class CarShopDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarImage> CarImages { get; set; }

        public CarShopDbContext() : base() { }

        public CarShopDbContext(DbContextOptions<CarShopDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;" +
                "Initial Catalog=CarShopDB;" +
                "Integrated Security=True;" +
                "Connect Timeout=30;" +
                "Encrypt=False;" +
                "TrustServerCertificate=False;" +
                "ApplicationIntent=ReadWrite;" +
                "MultiSubnetFailover=False");
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await Task.FromResult(Cars.ToList());
        }

        public async Task<TaskStatus> AddCarAsync(Car car)
        {
            await Cars.AddAsync(car);
            this.SaveChanges();
            return await Task.FromResult(Task.CompletedTask.Status);
        }

        public async Task<TaskStatus> AddImageAsync(Guid CarId, string imageName)
        {
            await CarImages.AddAsync(new CarImage() { CarId = CarId, Image = imageName });
            this.SaveChanges();
            return await Task.FromResult(Task.CompletedTask.Status);
        }

        public async Task<TaskStatus> RemoveImageAsync(string imageName)
        {
            CarImages.Remove(CarImages.FirstOrDefault(x => x.Image.Equals(imageName))!);
            this.SaveChanges();
            return await Task.FromResult(Task.CompletedTask.Status);
        }

        public async Task<List<CarImage>> GetCarImagesAsync()
        {
            return await Task.FromResult(CarImages.ToList());
        }

        public async Task<List<CarImage>> GetCarImagesByIdAsync(Guid carId)
        {
            return await Task.FromResult(CarImages.Where(x => x.CarId.Equals(carId)).ToList());
        }

        public async Task<Car> GetCarByIdAsync(Guid carId)
        {
            return await Task.FromResult(Cars.Where(x => x.CarId.Equals(carId)).FirstOrDefault());
        }

        public async Task<List<Car>> GetCarByRegionAsync(string RegionName)
        {
            return await Task.FromResult(Cars.Where(x => String.Compare(x.Region, RegionName) == 0).ToList());
        }

        public async Task RemoveCarByIdAsync(int removeCarId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCarByIdAsync(int updateCarId)
        {
            throw new NotImplementedException();
        }
    }
}

