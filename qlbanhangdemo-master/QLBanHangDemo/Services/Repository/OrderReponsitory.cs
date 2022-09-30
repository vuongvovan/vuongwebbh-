using Microsoft.EntityFrameworkCore;
using QLBanHangDemo.DAL;
using QLBanHangDemo.Models;
using QLBanHangDemo.Services.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLBanHangDemo.Services.Repository
{
    public class OrderReponsitory : Repository<Orders>, IOrderRepository
    {
        public OrderReponsitory(QLBanHangMVC2Context BHContext) : base(BHContext)
        {

        }

        public IEnumerable<Orders> GetDetailCustomerPaymentByOrderId()
        {
            return QLBanHangMVC2Context.Orders.Include(x => x.Customers).Include(x => x.OrderDetail).Include(x => x.Payment).ToList();
        }

        public Orders GetDetailCustomerPaymentByOrderId(int id)
        {
            return QLBanHangMVC2Context.Orders.Include(x => x.Customers).Include(x => x.OrderDetail).Include(x => x.Payment).FirstOrDefault(x => x.OrdersId == id);
        }
    }
}
