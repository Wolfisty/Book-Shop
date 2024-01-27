using BookShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BookShop.Domain.Entity;
using BookShop.DAL.Repositories;
using BookShop.DAL.Interfaces;
using BookShop.DAL;
using BookShop.Service.Interfaces;

namespace BookShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _bookService;

        private readonly ILogger<HomeController> _logger;
        ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context,IBookService bookService)
        {
            _bookService= bookService;
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var response = _bookService.GetBooks();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}