using BookShop.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Domain.Entity
{
    public class Books
    {
        public long Id { get; set; }
        public bool Bestseller { get; set; }
        public bool Noveltie { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string LongDescription { get; set; }
        public string ShortDescription { get; set; }
        public string Img { get; set; }
        public int Price { get; set; }
        public DateTime CreateDate { get; set; }
        public Category Category { get; set; }

    }
}
