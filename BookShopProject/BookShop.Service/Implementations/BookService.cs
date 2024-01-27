using BookShop.DAL.Interfaces;
using BookShop.Domain.Entity;
using BookShop.Domain.Enum;
using BookShop.Domain.Response;
using BookShop.Service.Interfaces;
using BookShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.WebSockets;
using BookShop.Domain.ViewModels.Books;
using Microsoft.Identity.Client;
using static BookShop.Service.Implementations.BookService;
using System.Runtime.ConstrainedExecution;
using Microsoft.EntityFrameworkCore;
using BookShop.Domain.Extensions;

namespace BookShop.Service.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBaseRepository<Books> _bookRepository;

        public BookService(IBaseRepository<Books> BooksRepository)
        {
            _bookRepository = BooksRepository;
        }


        public async Task<IBaseResponse<BooksViewModel>> GetBook(long id)
        {
            try
            {
                var book = await _bookRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (book == null)
                {
                    return new BaseResponse<BooksViewModel>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.DbNotFound
                    };
                }

                var data = new BooksViewModel()
                {
                    Title = book.Title,
                    Author = book.Author,
                    Price = book.Price,
                    ShortDescription = book.ShortDescription,
                    LongDescription = book.LongDescription,
                    Img = book.Img,
                    Bestseller = book.Bestseller,
                    Noveltie = book.Noveltie,
                    CreateDate = book.CreateDate,
                    Category = book.Category.GetDisplayName(),
                };

                return new BaseResponse<BooksViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<BooksViewModel>()
                {
                    Description = $"[GetBooks] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Dictionary<long, string>>> GetBook(string term)
        {
            var baseResponse = new BaseResponse<Dictionary<long, string>>();
            try
            {
                var book = await _bookRepository.GetAll()
                    .Select(x => new BooksViewModel()
                    {
                        Title = x.Title,
                        Author = x.Author,
                        Price = x.Price,
                        ShortDescription = x.ShortDescription,
                        LongDescription = x.LongDescription,
                        Img = x.Img,
                        Bestseller = x.Bestseller,
                        Noveltie = x.Noveltie,
                        CreateDate = x.CreateDate,
                        Category = x.Category.GetDisplayName() ,
                    })
                    .Where(x => EF.Functions.Like(x.Title, $"%{term}%"))
                    .ToDictionaryAsync(x => x.Id, t => t.Title);

                baseResponse.Data = book;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<long, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Books>> Create(BooksViewModel model)
        {
            try
            {
                var Books = new Books()
                {
                    Title = model.Title,
                    Author = model.Author,
                    Price = model.Price,
                    ShortDescription = model.ShortDescription,
                    LongDescription = model.LongDescription,
                    Img = model.Img,
                    Bestseller = model.Bestseller,
                    Noveltie = model.Noveltie,
                    CreateDate = model.CreateDate,
                    Category = (Category)Convert.ToInt32(model.Category),
                };
                await _bookRepository.Create(Books);

                return new BaseResponse<Books>()
                {
                    StatusCode = StatusCode.OK,
                    Data = Books
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Books>()
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteBook(long id)
        {
            try
            {
                var Books = await _bookRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (Books == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.BookNotFound,
                        Data = false
                    };
                }

                await _bookRepository.Delete(Books);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteBooks] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Books>> Edit(long id, BooksViewModel model)
        {
            try
            {
                var book = await _bookRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (book == null)
                {
                    return new BaseResponse<Books>()
                    {
                        Description = "Books not found",
                        StatusCode = StatusCode.BookNotFound
                    };
                }

                book.Title = model.Title;
                book.Author = model.Author;
                book.Price = model.Price;
                book.ShortDescription = model.ShortDescription;
                book.LongDescription = model.LongDescription;
                book.Img = model.Img;
                book.CreateDate = model.CreateDate;
                book.Bestseller = model.Bestseller;
                book.Noveltie = model.Noveltie;
                book.Category = (Category)Convert.ToInt32(model.Category);

                await _bookRepository.Update(book);


                return new BaseResponse<Books>()
                {
                    Data = book,
                    StatusCode = StatusCode.OK,
                };
                // TypeBooks
            }
            catch (Exception ex)
            {
                return new BaseResponse<Books>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<List<Books>> GetBooks()
        {
            try
            {
                
                var book = _bookRepository.GetAll().ToList();
                if (!book.Any())
                {
                    return new BaseResponse<List<Books>>()
                    {
                        Description = "Найдено 0 элементов",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<Books>>()
                {
                    Data = book,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Books>>()
                {
                    Description = $"[GetBookss] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

    }
}
