using QLBanHangDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLBanHangDemo.ViewModels
{
    public class ProductViewModel
    {

        public IEnumerable<Product> Product { get; set; }
        public IEnumerable<Category> Category { get; set; }
        public IEnumerable<Brand> Brand { get; set; }

    }
}
