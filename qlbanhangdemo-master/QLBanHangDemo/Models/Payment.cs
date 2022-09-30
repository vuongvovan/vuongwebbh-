using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLBanHangDemo.Models
{
    [Table("PAYMENT")]
    public partial class Payment
    {
        public Payment()
        {
            Orders = new HashSet<Orders>();
        }

        [Key]
        [Column("PAYMENT_ID")]
        public int PaymentId { get; set; }
        [Required]
        [Column("PAYMENT_METHOD")]
        [StringLength(250)]
        public string PaymentMethod { get; set; }
        [Required]
        [Column("PAYMENT_STATUS")]
        [StringLength(50)]
        public string PaymentStatus { get; set; }

        [InverseProperty("Payment")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
