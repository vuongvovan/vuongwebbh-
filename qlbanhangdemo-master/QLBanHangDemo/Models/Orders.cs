using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLBanHangDemo.Models
{
    [Table("ORDERS")]
    public partial class Orders
    {
        public Orders()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        [Key]
        [Column("ORDERS_ID")]
        public int OrdersId { get; set; }
        [Column("CUSTOMERS_ID")]
        public int CustomersId { get; set; }
        [Column("PAYMENT_ID")]
        public int PaymentId { get; set; }
        [Column("ORDER_TOTAL", TypeName = "decimal(18, 0)")]
        public decimal OrderTotal { get; set; }
        [Required]
        [Column("ORDER_STATUS")]
        [StringLength(50)]
        public string OrderStatus { get; set; }

        [ForeignKey(nameof(CustomersId))]
        [InverseProperty("Orders")]
        public virtual Customers Customers { get; set; }
        [ForeignKey(nameof(PaymentId))]
        [InverseProperty("Orders")]
        public virtual Payment Payment { get; set; }
        [InverseProperty("Orders")]
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
