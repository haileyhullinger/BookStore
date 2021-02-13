using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class BookDBContext : DbContext
    {
        //constructor
        public BookDBContext (DbContextOptions<BookDBContext> options) : base (options)
        {

        }
        //a set of books
        public DbSet<Book> Books { get; set; }
    }
}
