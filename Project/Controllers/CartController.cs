using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Services;

namespace Project.Controllers
{
    public class CartController : Controller
    {
        //A Helper to identify Hosting side path info 
        private readonly IHostingEnvironment _hostingEnvironment;
        private IDataService<Address> _addressDataService;
        private IDataService<Profile> _profileDataService;
        private UserManager<IdentityUser> _userManagerService;
        private readonly IDataService<Hamper> _hamperService;

        public CartController(IHostingEnvironment hostingEnvironment,
                                 IDataService<Hamper> hamperService,
                                 IDataService<Address> addressService,
                                IDataService<Profile> profileService,
                                 UserManager<IdentityUser> userManager)
        {
            _hostingEnvironment = hostingEnvironment;
            _hamperService = hamperService;
            _addressDataService = addressService;
            _profileDataService = profileService;
            _userManagerService = userManager;
        }
        // GET: /<controller>/
        [Authorize]
        public async Task<IActionResult> Index()
        {
            //get the user who already logged in
            IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
            //get the profile for this user
            Profile profile = _profileDataService.GetSingle(p => p.UserId == user.Id);
            //get all addresses of this user
            List<Address> addressList = _addressDataService.Query(p => p.ProfileId == profile.ProfileId).ToList();
            addressList.Insert(0, new Address { AddressId = 0, AddressLine = "Select an address" });
            ViewBag.addressList = addressList;

            //Retrieving the cart items from the session 
            var cart = SessionHelper.GetObjectFromJson<List<OrderLineItem>>(HttpContext.Session, "cart");
            //passing the cart to the ViewBag [ViewBag is a bag holding the data to share with view]
            ViewBag.cart = cart;
            if (cart != null)
            {
                //You can calcualte the total of your selected items in cart 
                ViewBag.total = cart.Sum(item => item.Hamper.Price * item.Quantity);
            }
            return View();
        }
        [Authorize]
        [Route("buy/{id}")]
        public IActionResult Buy(int id)
        {
            //We are checking if any product is previously added to the cart or not
            if (SessionHelper.GetObjectFromJson<List<OrderLineItem>>(HttpContext.Session, "cart") == null)
            {
                //If there is not item exist in the session 
                List<OrderLineItem> cart = new List<OrderLineItem>();
                cart.Add(new OrderLineItem { Hamper = _hamperService.GetSingle(h => h.HamperId == id), Quantity = 1 });
                //You saving your List of OrderItems inside the Sesson with a key called "cart" as a json data
                //Because json is basically string 
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                //If already there are OrderItems exist in the Session
                //We first retrieving them and checking
                List<OrderLineItem> cart = SessionHelper.GetObjectFromJson<List<OrderLineItem>>(HttpContext.Session, "cart");
                //We have a method name IsExist which taking an argument is the product id
                //We checking if any product with that particular id is already added to the cart or not
                int index = IsExist(id);
                if (index != -1)
                {
                    //If already exist 
                    //Just increasing the quantity
                    cart[index].Quantity++;
                }
                else
                {
                    //If not exist
                    //I am adding the new orderItem to the existing list
                    cart.Add(new OrderLineItem { Hamper = _hamperService.GetSingle(h => h.HamperId == id), Quantity = 1 });
                }
                //I am adding the new orderItem to the existing list
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<OrderLineItem> cart = SessionHelper.GetObjectFromJson<List<OrderLineItem>>(HttpContext.Session, "cart");
            int index = IsExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        private int IsExist(int id)
        {
            List<OrderLineItem> cart = SessionHelper.GetObjectFromJson<List<OrderLineItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Hamper.HamperId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

        [HttpGet]
        public IActionResult OrderComplete()
        {
            return View();
        }
    }
}