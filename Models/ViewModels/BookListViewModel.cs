using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels
{
    public class BookListViewModel
    {
        //the Books gets populated in the home controller
        public IEnumerable<Book> Books { get; set; }
        public PagingInfo PagingInfo { get; set; }
        //classification to search by (category)
        public string CurrentClassification { get; set; }
    }
}
