using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Project.Models;
using Project.Services;
using Project.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Project.Controllers
{
    public class AccountController : Controller
    {
        private IDataService<Profile> _profileDataService;
        private UserManager<IdentityUser> _userManagerService;
        private SignInManager<IdentityUser> _signInManagerService;
        private RoleManager<IdentityRole> _roleManagerService;

        public AccountController(IDataService<Profile> profileService,
                                 UserManager<IdentityUser> userManagerService,
                                 SignInManager<IdentityUser> signInManagerService,
                                 RoleManager<IdentityRole> roleManagerService
                                )
        {
            _profileDataService = profileService;
            _userManagerService = userManagerService;
            _signInManagerService = signInManagerService;
            _roleManagerService = roleManagerService;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //add a user to users table
                IdentityUser user = new IdentityUser(vm.UserName);
                user.Email = vm.Email;
                IdentityResult result = await _userManagerService.CreateAsync(user, vm.Password);

                if (result.Succeeded)
                {
                    Profile profile = new Profile
                    {
                        UserId = user.Id,
                        FirstName = vm.FirstName,
                        LastName = vm.LastName,
                        Dob = vm.Dob,
                        Phone = vm.Phone 
                    };
                    _profileDataService.Create(profile);

                    //go home
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //show error
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            AccountLoginViewModel vm = new AccountLoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManagerService.PasswordSignInAsync(vm.Username, vm.Password, vm.RememberMe, false);

                if (result.Succeeded)
                {
                    if (String.IsNullOrEmpty(vm.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect(vm.ReturnUrl);
                    }
                }
                ModelState.AddModelError("", "Username or Password is incorrect");
            }
            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
           await _signInManagerService.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfile()
        {
            //get the user who already logged in
            IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
            //get the profile for this user
            Profile profile = _profileDataService.GetSingle(p => p.UserId == user.Id);
            //create vm
            AccountUpdateProfileViewModel vm = new AccountUpdateProfileViewModel
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Email = user.Email,
                Dob = profile.Dob,
                Phone = profile.Phone
            };
            return View(vm);

        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(AccountUpdateProfileViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //get the user who already logged in
                IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
                //get the profile for this user
                Profile profile = _profileDataService.GetSingle(p => p.UserId == user.Id);
                //map the vm
                profile.UserId = user.Id;
                profile.FirstName = vm.FirstName;
                profile.LastName = vm.LastName;
                profile.Dob = vm.Dob;
                profile.Phone = vm.Phone;

                //save changes
                _profileDataService.Update(profile);
                //update email
                user.Email = vm.Email;
                await _userManagerService.UpdateAsync(user);
                //go home
                return RedirectToAction("Index", "Home");
            }
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRole(AccountAddRoleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole(vm.Name);
                IdentityResult result = await _roleManagerService.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult Denied()
        {
            return View();
        }
    }
}