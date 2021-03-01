using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels
{
    public class PagingInfo
    {

        public int TotalNumItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage {get;set;}

        //calcualte how many total pages need to have, based on num items (rounded up)/items perpage
        public int TotalPages => (int)(Math.Ceiling((decimal) TotalNumItems / ItemsPerPage));

    }
}
