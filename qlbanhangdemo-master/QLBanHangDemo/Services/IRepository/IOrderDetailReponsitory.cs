using QLBanHangDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLBanHangDemo.Services.IRepository
{
    public interface IOrderDetailReponsitory : IRepository<OrderDetail>
    {
        IEnumerable<OrderDetail> GetProductOredrByOrderDetailId();
        OrderDetail GetProductOredrByOrderDetailId(int id);
    }
}
