using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLBanHangDemo.Models
{
    [Table("BRAND")]
    public partial class Brand
    {
        public Brand()
        {
            Product = new HashSet<Product>();
        }

        [Key]
        [Column("BRAND_ID")]
        public int BrandId { get; set; }
        [Required]
        [Column("BRAND_NAME")]
        [StringLength(100)]
        public string BrandName { get; set; }
        [Column("BRAND_DESC")]
        [StringLength(500)]
        public string BrandDesc { get; set; }

        [InverseProperty("Brand")]
        public virtual ICollection<Product> Product { get; set; }
    }
}
