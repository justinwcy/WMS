using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WMS.Areas.Identity.Data;
using WMS.Areas.Identity.Pages.Account;
using WMS.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("WarehouseUserDbContextConnection") ?? throw new InvalidOperationException("Connection string 'WarehouseUserDbContextConnection' not found.");

builder.Services.AddDbContext<WarehouseUserDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<WarehouseUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<WarehouseUserDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// configure the minimum requirement for password
builder.Services.Configure<IdentityOptions>(options =>
{
    // we get the minimum length from the input model of register page
    var registerInputModelType = typeof(RegisterModel.InputModel);
    var passwordProperty = registerInputModelType.GetProperty("Password");
    var passwordStringLengthAttribute = 
        (StringLengthAttribute[])passwordProperty.GetCustomAttributes(typeof(StringLengthAttribute), false);
    options.Password.RequiredLength = passwordStringLengthAttribute[0].MinimumLength;
});

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
// add this for the Identity framework
app.MapRazorPages();

app.Run();
