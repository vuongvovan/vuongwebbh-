using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLBanHangDemo.Models
{
    [Table("PRODUCT")]
    public partial class Product
    {
        public Product()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        [Key]
        [Column("PRODUCT_ID")]
        public int ProductId { get; set; }
        [Column("CATEGORY_ID")]
        [Required(ErrorMessage = "Chưa có danh mục")]
        public int CategoryId { get; set; }
        [Column("BRAND_ID")]
        [Required(ErrorMessage = "Chưa có Brand")]
        public int BrandId { get; set; }
        [Required]
        [Column("PRODUCT_NAME")]
        [StringLength(200)]
        public string ProductName { get; set; }
        [Column("PRODUCT_DESC")]
        [StringLength(200)]
        public string ProductDesc { get; set; }
        [Column("PRODUCT_IMAGE")]
        [StringLength(200)]
        public string ProductImage { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
        [Column("PRODUCT_PRICE", TypeName = "decimal(18, 0)")]
        public decimal ProductPrice { get; set; }
        [Column("PRODUCT_STATUS")]
        [StringLength(50)]
        public string ProductStatus { get; set; }

       

        [ForeignKey(nameof(BrandId))]
        [InverseProperty("Product")]
        public virtual Brand Brand { get; set; }
        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Product")]
        public virtual Category Category { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
