using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using CarShopOnline_v3.Data;
using CarShopOnline_v3.Models.user;
using CarShopOnline_v3.Areas.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("CarShopContextConnection") ?? throw new InvalidOperationException("Connection string 'CarShopContextConnection' not found.");

builder.Services.AddDbContext<CarShopContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<CarShopUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<CarShopContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
