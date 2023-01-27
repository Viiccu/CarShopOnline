using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CarShopOnline_v3.Models.user;

namespace CarShopOnline_v3.Data;

public class CarShopContext : IdentityDbContext<CarShopUser>
{
    public CarShopContext(DbContextOptions<CarShopContext> options)
        : base(options)
    {
    }
}
