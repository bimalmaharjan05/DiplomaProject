using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.ViewModels;
using Project.Models;
using Project.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Project.Controllers
{
    public class HamperController : Controller
    {
        private IDataService<Hamper> _hamperDataService;
        private IDataService<Category> _categoryDataService;
        private IHostingEnvironment _environment;
        public HamperController(IDataService<Hamper> hamperService,
                                IDataService<Category> categoryService,
                                IHostingEnvironment environment)
        {
            _hamperDataService = hamperService;
            _categoryDataService = categoryService;
            _environment = environment;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(int id)
        {
            HamperCreateViewModel vm = new HamperCreateViewModel
            {
                CategoryId = id
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(HamperCreateViewModel vm, IFormFile picture)
        {
            Hamper hamper = new Hamper();
            if (picture != null)
            {
                //Create a path including the filename where we want to save the file 
                var fileName = Path.Combine(_environment.WebRootPath, "uploads", Path.GetFileName(picture.FileName));
                //copy the file from temp memory to a permanent memory
                var fileStream = new FileStream(fileName, FileMode.Create);
                await picture.CopyToAsync(fileStream);
                //Whenever you use any System.IO interface or classes you makesure you close the process;
                fileStream.Close();
                hamper.Picture = Path.GetFileName(picture.FileName);
            }
            if (ModelState.IsValid)
            {
                hamper.CategoryId = vm.CategoryId;
                hamper.HamperName = vm.HamperName;
                hamper.Price = vm.Price;
                hamper.HamperCategory = vm.CategoryName;
                hamper.HamperDetails = vm.HamperDetails;

                //save to db
                _hamperDataService.Create(hamper);
                //go to home
                return RedirectToAction("Details", "Category", new { id = vm.CategoryId });
            }
            else
            {
                return View(vm);
            }

        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult Update(int id)
        {
            Hamper hamper = _hamperDataService.GetSingle(h => h.HamperId == id);

                //map
                HamperUpdateViewModel vm = new HamperUpdateViewModel
                {
                    HamperId = id,
                    CategoryId = hamper.CategoryId,
                    HamperName = hamper.HamperName,
                    HamperDetails = hamper.HamperDetails,
                    Picture = hamper.Picture,
                    Price = hamper.Price,
                    CategoryName = hamper.HamperCategory,
                    Discontinued = hamper.Discontinued   
                };
                return View(vm);
            
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Update(HamperUpdateViewModel vm, IFormFile picture)
        {
            //map
            Hamper updatedHamper = new Hamper
            {
                HamperId = vm.HamperId,
                CategoryId = vm.CategoryId,
                HamperCategory = vm.CategoryName,
                HamperName = vm.HamperName,
                HamperDetails = vm.HamperDetails,
                Price = vm.Price,
                Discontinued = vm.Discontinued
            };

            string prevPicturePath = null;

            if (picture != null)
            {
                //Checking if previously any avatar was uploaded or not
                if (updatedHamper.Picture != null)
                {
                    //As there was an avatar before so you have to delete it first 
                    prevPicturePath = Path.Combine(_environment.WebRootPath, "images", updatedHamper.Picture);
                    System.IO.File.Delete(prevPicturePath);
                }

                var fileName = Path.Combine(_environment.WebRootPath, "uploads", Path.GetFileName(picture.FileName));
                var fileStream = new FileStream(fileName, FileMode.Create);
                await picture.CopyToAsync(fileStream);
                fileStream.Close();

                updatedHamper.Picture = Path.GetFileName(picture.FileName);
            }
            _hamperDataService.Update(updatedHamper);
            vm.Picture = updatedHamper.Picture;
 
            //go to 
            return RedirectToAction("Details", "Category", new { id = vm.CategoryId });
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult Delete(int id)
        {
            _hamperDataService.Delete(new Hamper { HamperId = id });
            return RedirectToAction("Index", "Category");
        }

        

        //[HttpPost]
        //public IActionResult Search(HamperSearchViewModel vm)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Category cat = _categoryDataService.GetSingle(c => c.CategoryName.ToLower() == vm.CategoryName.ToLower());

        //        if (cat != null)
        //        {
        //            //IEnumerable<Hamper> list = _hamperDataService.Query(h => h.CategoryId == cat.CategoryId);
        //            //return View(list.ToList());
        //            return RedirectToAction("Details", "Category", new { id = cat.CategoryId });
        //        }
        //    }
        //    return View(vm);
        //}

        
    }
}