using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLBanHangDemo.Models
{
    [Table("CUSTOMERS")]
    public partial class Customers
    {
        public Customers()
        {
            Orders = new HashSet<Orders>();
        }

        [Key]
        [Column("CUSTOMERS_ID")]
        public int CustomersId { get; set; }
        [Required]
        [Column("CUSTOMERS_NAME")]
        [StringLength(50)]
        public string CustomersName { get; set; }
        [Required]
        [Column("CUSTOMERS_PHONE")]
        [StringLength(10)]
        public string CustomersPhone { get; set; }
        [Required]
        [Column("CUSTOMERS_EMAIL")]
        [StringLength(20)]
        public string CustomersEmail { get; set; }


        [InverseProperty("Customers")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
