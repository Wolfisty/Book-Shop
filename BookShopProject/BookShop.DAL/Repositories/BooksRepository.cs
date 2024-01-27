using BookShop.DAL.Interfaces;
using BookShop.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.DAL.Repositories
{
    public class BooksRepository : IBaseRepository<Books>
    {
        private readonly ApplicationDbContext _db;

        public BooksRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Books entity)
        {
            await _db.Books.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Books> GetAll()
        {
            return _db.Books;
        }

        public async Task Delete(Books entity)
        {
            _db.Books.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Books> Update(Books entity)
        {
            _db.Books.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
