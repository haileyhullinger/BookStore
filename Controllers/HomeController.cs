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
        
        //added to receive the IBookRepository
        public HomeController(ILogger<HomeController> logger, IBookRepository repository)
        {
            _logger = logger;
            //added
            _repository = repository;
        }

        public IActionResult Index()
        {
            //when view is called, the books will be passed into the view
            return View(_repository.Books);
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
