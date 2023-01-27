using CarShopOnline_v3.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CarShopOnline_v3.Models.user
{
    public class CarShopUser : IdentityUser, ICarShopUser
    {
        public string Region { get; set; } = null!;
        public string BgImage { get; set; } = null!;
    }
}
