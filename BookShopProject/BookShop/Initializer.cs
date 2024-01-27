using BookShop.DAL.Interfaces;
using BookShop.DAL.Repositories;
using BookShop.Domain.Entity;
using BookShop.Service.Implementations;
using BookShop.Service.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;
using NuGet.ContentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookShop
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<Books>, BooksRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
        }
    }
}
