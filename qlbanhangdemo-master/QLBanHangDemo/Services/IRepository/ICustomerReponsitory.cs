using QLBanHangDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLBanHangDemo.Services.IRepository
{
    public interface ICustomerReponsitory : IRepository<Customers>
    {
        IEnumerable<Customers> GetCustomerCustomerId(int CustomerId);
    }
}
