using BookShop.DAL;
using BookShop.DAL.Interfaces;
using BookShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BookShop.Domain.Response;
using Microsoft.AspNetCore.Authorization;
using BookShop.Domain.ViewModels.Books;
using Microsoft.AspNetCore.Cors.Infrastructure;
using BookShop.Models;
using System.Diagnostics;

namespace BookShop.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public BookController(IBookService BookService)
        {
            _bookService = BookService;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            var response = _bookService.GetBooks();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _bookService.DeleteBook(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetBooks");
            }
            return RedirectToAction("Error");
        }

        public IActionResult Compare() => PartialView();

        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
                return PartialView();

            var response = await _bookService.GetBook(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView(response.Data);
            }
            ModelState.AddModelError("", response.Description);
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Save(BooksViewModel viewModel)
        {
            ModelState.Remove("Id");
            ModelState.Remove("DateCreate");
            if (ModelState.IsValid)
            {
                if (viewModel.Id == 0)
                {
                    
                    await _bookService.Create(viewModel);
                }
                else
                {
                    await _bookService.Edit(viewModel.Id, viewModel);
                }
            }
            return RedirectToAction("GetBooks");
        }


        [HttpGet]
        public async Task<ActionResult> GetBook(int id, bool isJson)
        {
            var response = await _bookService.GetBook(id);
            if (isJson)
            {
                return View(response.Data);
            }
            return PartialView("GetBook", response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> GetBook(string term)
        {
            var response = await _bookService.GetBook(term);
            return Json(response.Data);
        }
    }
}
