using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLBanHangDemo.Models
{
    [Table("CATEGORY")]
    public partial class Category
    {
        public Category()
        {
            Product = new HashSet<Product>();
        }

        [Key]
        [Column("CATEGORY_ID")]
        public int CategoryId { get; set; }
        [Column("CATEGORY_NAME")]
        [StringLength(100)]
        public string CategoryName { get; set; }
        [Column("CATEGORY_DESC")]
        [StringLength(500)]
        public string CategoryDesc { get; set; }

        [InverseProperty("Category")]
        public virtual ICollection<Product> Product { get; set; }
    }
}
