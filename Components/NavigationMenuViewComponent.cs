using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        //bring in model
        private IBookRepository repository;

        public NavigationMenuViewComponent (IBookRepository repo)
        {
            repository = repo;
        }

        //drop a partial view into the view, selecting all of the classifications of books
        public IViewComponentResult Invoke()
        {
            //viewbag that will hold the classification selected in order to dynamically highlight the classification that is currently selected
            ViewBag.SelectedClassification = RouteData?.Values["classification"];

            //select each type of classification to use as naviation menu options
            return View(repository.Books
                .Select(x => x.Classification)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
