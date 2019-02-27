using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Services;
using Project.ViewModels;

namespace Project.Controllers
{
    public class AddressController : Controller
    {
        private IDataService<Address> _addressDataService;
        private IDataService<Profile> _profileDataService;
        private UserManager<IdentityUser> _userManagerService;
        public AddressController(IDataService<Address> addressService, 
                                 IDataService<Profile> profileService,
                                 UserManager<IdentityUser> userManager
                                   )
        {
            _addressDataService = addressService;
            _profileDataService = profileService;
            _userManagerService = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            //get the user who already logged in
            IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
            //get the profile for this user
            Profile profile = _profileDataService.GetSingle(p => p.UserId == user.Id);

            //get all addresses of this user
            IEnumerable<Address> addresses = _addressDataService.Query(p => p.ProfileId == profile.ProfileId);

            AddressIndexViewModel vm = new AddressIndexViewModel
            {
                Profile = profile,
                Addresses = addresses  
            };
            return View(vm);
        }
        [HttpGet]
        [Authorize]
        public IActionResult Create(int id)
        {
            AddressCreateViewModel vm = new AddressCreateViewModel
            {
                ProfileId = id
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(AddressCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Address address = new Address
                {
                    ProfileId = vm.ProfileId,
                    AddressLine = vm.AddressLine,
                    Suburb = vm.Suburb,
                    State = vm.State,
                    PostCode = vm.PostCode
                };

                //save to db
                _addressDataService.Create(address);

                //go to home
                return RedirectToAction("Index", "Address", new { id = vm.ProfileId });
            }
            else
            {
                return View(vm);
            }
            
        }

        [HttpGet]
        [Authorize]
        public IActionResult Update(int id)
        {
            Address address = _addressDataService.GetSingle(a => a.AddressId == id);

            //map
            AddressUpdateViewModel vm = new AddressUpdateViewModel
            {
                AddressId = id,
                ProfileId = address.ProfileId,
                AddressLine = address.AddressLine,
                Suburb = address.Suburb,
                State = address.State,
                PostCode = address.PostCode
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Update(AddressUpdateViewModel vm)
        {
            Address updatedAddress = new Address
            {
                AddressId = vm.AddressId,
                ProfileId = vm.ProfileId,
                AddressLine = vm.AddressLine,
                Suburb = vm.Suburb,
                State = vm.State,
                PostCode = vm.PostCode
            };

            _addressDataService.Update(updatedAddress);

            return RedirectToAction("Index", "Address", new { id = vm.ProfileId });
        }

        [HttpGet]
        [Authorize]
        public IActionResult Delete(int id)
        {
            _addressDataService.Delete(new Address { AddressId = id });
            return RedirectToAction("Index", "Address");
        }
    }
}