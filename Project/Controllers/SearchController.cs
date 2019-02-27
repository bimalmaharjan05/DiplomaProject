using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Services;
using Project.ViewModels;

namespace Project.Controllers
{
    public class SearchController : Controller
    {
        private IDataService<Hamper> _hamperDataService;
        private IDataService<Category> _categoriesDataService;

        public SearchController(IDataService<Hamper> hamperDataService,
                                IDataService<Category> categoriesDataService)

        {
            _hamperDataService = hamperDataService;
            _categoriesDataService = categoriesDataService;

        }
        [HttpGet]
        public IActionResult Index(int id, double minPrice, double maxPrice)
        {
            IEnumerable<Hamper> hampers = _hamperDataService.Query(h => h.CategoryId == id);
            IEnumerable<Category> categories = _categoriesDataService.GetAll();

            SearchIndexViewModel vm = new SearchIndexViewModel
            {
                Hampers = hampers,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                Categories = categories
            };
            if (minPrice != 0 || maxPrice != 0)
            {
                vm.Hampers = _hamperDataService.GetAll().Where(h => h.Price >= minPrice && h.Price <= maxPrice).Where(h => h.CategoryId == id);
            }
            return View(vm);
        }
    }
}