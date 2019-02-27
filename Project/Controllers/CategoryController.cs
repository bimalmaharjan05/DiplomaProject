using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Services;
using Project.ViewModels;

namespace Project.Controllers
{
    public class CategoryController : Controller
    {
        private IDataService<Category> _categoryDataService;
        private IDataService<Hamper> _hamperDataService;
        public CategoryController(IDataService<Category> categoryService,
                                  IDataService<Hamper> hamperService)
        {
            _categoryDataService = categoryService;
            _hamperDataService = hamperService;
        }
        [HttpGet]
        [Authorize (Roles="Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(CategoryCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //check exists - Name = unique
                Category existingCategory = _categoryDataService.GetSingle(c => c.CategoryName == vm.CategoryName);
                if (existingCategory == null)
                {
                    Category cat = new Category
                    {
                        CategoryName = vm.CategoryName
                    };

                    //save to db
                    _categoryDataService.Create(cat);
                    //go home
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.MyMessage = "Category name exists. Please change the name";
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
                
        }

        public IActionResult Index()
        {
            //call service
            IEnumerable<Category> cats = _categoryDataService.GetAll();
            //creat vm
            CategoryIndexViewModel vm = new CategoryIndexViewModel
            {
                Total = cats.Count(),
                Categories = cats
            };
            //pass vm to view
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult Update(int id)
        {
            //call service
            Category cat = _categoryDataService.GetSingle(c => c.CategoryId == id);

            CategoryUpdateViewModel vm = new CategoryUpdateViewModel
            {
                CategoryId = cat.CategoryId,
                CategoryName = cat.CategoryName
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public IActionResult Update(CategoryUpdateViewModel vm)
        {
            //map
            Category updatedCat = new Category
            {
                CategoryId = vm.CategoryId,
                CategoryName = vm.CategoryName
            };

            //call service
            _categoryDataService.Update(updatedCat);
            //
            return RedirectToAction("Index", "Category", new { id = vm.CategoryId });
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult Details(int id)
        {
            //get single category
            Category cat = _categoryDataService.GetSingle(c => c.CategoryId == id);
            //get the list of products by id
            IEnumerable<Hamper> hamperList = _hamperDataService.Query(h => h.CategoryId == id);

            //create vm
            CategoryDetailsViewModel vm = new CategoryDetailsViewModel
            {
                Total = hamperList.Count(),
                Name = cat.CategoryName,
                CategoryId = cat.CategoryId,
                Hampers = hamperList
            };

            //pass to view
            return View(vm);
        }

        [HttpGet]
        public IActionResult Shop(int id)
        {
            //get single category
            Category cat = _categoryDataService.GetSingle(c => c.CategoryId == id);
            //get the list of products by id
            IEnumerable<Hamper> hamperList = _hamperDataService.Query(h => h.CategoryId == id).Where(h => h.Discontinued == false);

            //create vm
            CategoryDetailsViewModel vm = new CategoryDetailsViewModel
            {
                Total = hamperList.Count(),
                Name = cat.CategoryName,
                CategoryId = cat.CategoryId,
                Hampers = hamperList
            };

            //pass to view
            return View(vm);
        }

    }
}