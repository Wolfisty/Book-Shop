using Microsoft.EntityFrameworkCore;
using BookShop.DAL;
using BookShop.DAL.Interfaces;
using BookShop.DAL.Repositories;
using BookShop.Service.Interfaces;
using BookShop.Service.Implementations;
using BookShop.DAL.Repositories;
using BookShop.Domain.Entity;
using BookShop;

var builder = WebApplication.CreateBuilder(args);
// получаем строку подключения из файла конфигурации
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddScoped<IBaseRepository<Books>, BooksRepository>();
builder.Services.AddScoped<IBookService, BookService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.Run();
