using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using QLBanHangDemo.DAL;
using QLBanHangDemo.Models;

namespace QLBanHangDemo.DAL
{
    public partial class QLBanHangMVC2Context : IdentityDbContext
    {
        public QLBanHangMVC2Context(DbContextOptions options)
             : base(options)
        {
        }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-R346NSM\\SQLEXPRESS;Initial Catalog=QLBanHangMVC;Integrated Security=True");
            }
        }
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.ApplyConfiguration(new BrandConfig());

            modelBuilder.ApplyConfiguration(new CatergoryConfig());

            modelBuilder.ApplyConfiguration(new CustomerConfig());

            modelBuilder.ApplyConfiguration(new OrderConfig());

            modelBuilder.ApplyConfiguration(new OrderDetailConfig());

            modelBuilder.ApplyConfiguration(new PaymentConfig());

            modelBuilder.ApplyConfiguration(new ProductConfig());
        }
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-R346NSM\\SQLEXPRESS;Initial Catalog=QLBanHangMVC;Integrated Security=True");
            }
        }
        */
        public DbSet<QLBanHangDemo.ViewModels.RoleViewModel> RoleViewModel { get; set; }
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-R346NSM\\SQLEXPRESS;Initial Catalog=QLBanHangMVC;Integrated Security=True");
            }
        }
        */
        public DbSet<QLBanHangDemo.ViewModels.UserViewModel> UserViewModel { get; set; }


    }
}
