using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MasterlistOfBusiness.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MOBContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MOBContext") ?? throw new InvalidOperationException("Connection string 'MOBContext' not found.")));


builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.LoginPath = "/Uzytkownik/Login"; 
        options.Cookie.Name = "MyCookieAuth";
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();


// Tworzenie domyślnego admina przy pierwszym uruchomieniu
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MOBContext>();
    if (!context.Uzytkownik.Any())
    {
        var admin = new MasterlistOfBusiness.Models.Uzytkownik
        {
            login = "admin",
            HasloHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
            Haslo = "", // To jest tylko do weryfikacji, nie powinno być przechowywane w bazie
            typ = "Admin"
        };
        context.Uzytkownik.Add(admin);
        context.SaveChanges();
        Console.WriteLine("Admin user created.");
    }
    else
    {
        Console.WriteLine("Admin user already exists.");
    }
}


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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Uzytkownik}/{action=Login}/{id?}");

app.Run();
