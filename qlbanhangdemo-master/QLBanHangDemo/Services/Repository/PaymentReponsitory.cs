using QLBanHangDemo.DAL;
using QLBanHangDemo.Models;
using QLBanHangDemo.Services.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLBanHangDemo.Services.Repository
{
    public class PaymentReponsitory: Repository<Payment>, IPaymentReponsitory
    {
        public PaymentReponsitory(QLBanHangMVC2Context BHContext) : base(BHContext)
        {

        }
    }
}
