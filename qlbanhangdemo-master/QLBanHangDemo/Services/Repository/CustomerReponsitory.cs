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
    public class CustomerReponsitory : Repository<Customers>, ICustomerReponsitory
    {
        public CustomerReponsitory(QLBanHangMVC2Context BHContext) : base(BHContext)
        {

        }

        public IEnumerable<Customers> GetCustomerCustomerId(int CustomerId)
        {
            return QLBanHangMVC2Context.Customers
               .Where(e => e.CustomersId == CustomerId)
               .Include(x => x.CustomersId).Include(x => x.CustomersName)
               .Include(x => x.CustomersPhone).Include(x => x.CustomersEmail)
               .ToList();
        }
    }
}
