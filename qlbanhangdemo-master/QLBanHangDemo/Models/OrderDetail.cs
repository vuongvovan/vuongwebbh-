using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLBanHangDemo.Models
{
    [Table("ORDER_DETAIL")]
    public partial class OrderDetail
    {
        [Key]
        [Column("ORDER_DETAIL_ID")]
        public int OrderDetailId { get; set; }
        [Key]
        [Column("ORDERS_ID")]
        public int OrdersId { get; set; }
        [Key]
        [Column("PRODUCT_ID")]
        public int ProductId { get; set; }
        [Required]
        [Column("PRODUCT_NAME")]
        [StringLength(200)]
        public string ProductName { get; set; }
        [Column("PRODUCT_PRICE", TypeName = "decimal(18, 0)")]
        public decimal ProductPrice { get; set; }
        [Column("PRODUCT_SALES_QUANTITY")]
        public int ProductSalesQuantity { get; set; }

        [ForeignKey(nameof(OrdersId))]
        [InverseProperty("OrderDetail")]
        public virtual Orders Orders { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("OrderDetail")]
        public virtual Product Product { get; set; }
    }
}
