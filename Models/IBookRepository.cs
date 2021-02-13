using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public interface IBookRepository
    {
        IQueryable<Book> Books { get; }
    }
}
