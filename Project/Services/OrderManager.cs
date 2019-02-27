using Microsoft.EntityFrameworkCore;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Services
{
    public class OrderManager
    {
        private MyDbContext _context;
        private DbSet<Order> _dbOrder;

        public OrderManager()
        {
            _context = new MyDbContext();
            _dbOrder = _context.Set<Order>();
        }

        public Order AddHamper(int orderId, int hamperId, int qty)
        {
            Order dbOrder = _dbOrder.Where(o => o.OrderId == orderId)
                                    .Include(o => o.Hampers).FirstOrDefault();
            Hamper dbHamper = _context.TblHamper.Where(h => h.HamperId == hamperId).FirstOrDefault();

            dbOrder.Hampers.Add(new OrderLineItem { Hamper = dbHamper, Quantity = qty });
            _context.SaveChanges();
            return dbOrder;
        }
    }
}
