using BookShop.Domain.Entity;
using BookShop.Domain.Response;
using BookShop.Domain.ViewModels.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Service.Interfaces
{
    public interface IBookService
    {
        IBaseResponse<List<Books>> GetBooks();

        Task<IBaseResponse<BooksViewModel>> GetBook(long id);

        Task<BaseResponse<Dictionary<long, string>>> GetBook(string term);

        Task<IBaseResponse<Books>> Create(BooksViewModel car);

        Task<IBaseResponse<bool>> DeleteBook(long id);

        Task<IBaseResponse<Books>> Edit(long id, BooksViewModel model);
    }
}
