using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //added
        private IBookRepository _repository;

        //added for pagination
        public int PageSize = 5;
        
        //added to receive the IBookRepository
        public HomeController(ILogger<HomeController> logger, IBookRepository repository)
        {
            _logger = logger;
            //added
            _repository = repository;
        }

        public IActionResult Index(string classification, int pageNum = 1)
        {
            //when view is called, the books will be passed into the view, based on the items per page
            //query in a language called LINK
            return View(new BookStore.Models.ViewModels.BookListViewModel
            {
                Books = _repository.Books
                //when a category is inputted, then it filters. If there is no classification entered, then it wont filter
                    .Where(p => classification == null || p.Classification == classification)
                    .OrderBy(p => p.BookId)
                    .Skip((pageNum - 1) * PageSize)
                    .Take(PageSize)
                    ,
                PagingInfo = new Models.ViewModels.PagingInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = PageSize,
                    //if the classification is null, count all the books. If a classification is selected, only count the books that match that classification
                    TotalNumItems = classification == null ? _repository.Books.Count() :
                                    _repository.Books.Where (x => x.Classification == classification).Count()
                },

                //like what page is being selected, but this is sorting by a classification of book. can be set in the url (classification = comedy)
                CurrentClassification = classification
            }); 
                
                
                
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
