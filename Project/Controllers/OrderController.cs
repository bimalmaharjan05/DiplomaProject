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
    public class OrderController : Controller
    {
        private IDataService<Order> _orderDataService;
        public OrderController(IDataService<Order> orderService)
        {
            _orderDataService = orderService;
        }

        [HttpPost]
        public IActionResult PlaceOrder(OrderIndexViewModel vm)
        {
            OrderManager orderManager = new OrderManager();
            List<OrderLineItem> cart = SessionHelper.GetObjectFromJson<List<OrderLineItem>>(HttpContext.Session, "cart");

            Order order = new Order
            {
                OrderDate = DateTime.Today,
                AddressId = vm.AddressId
            };
            _orderDataService.Create(order);

            foreach (OrderLineItem item in cart)
            {

                orderManager.AddHamper(order.OrderId, item.Hamper.HamperId, item.Quantity);
            }

            
            return RedirectToAction("OrderComplete", "Cart");
        }


    }
}