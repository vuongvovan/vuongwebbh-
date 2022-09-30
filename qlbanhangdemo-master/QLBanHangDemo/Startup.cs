using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QLBanHangDemo.DAL;
using QLBanHangDemo.Services.IRepository;
using QLBanHangDemo.Services.Repository;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLBanHangDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<QLBanHangMVC2Context>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });

            services.AddTransient<IProductRepository, ProductReponsitory>();
            services.AddTransient<IPaymentReponsitory, PaymentReponsitory>();
            services.AddTransient<IBrandRepository, IBrandReponsitory>();
            services.AddTransient<ICustomerReponsitory, CustomerReponsitory>();
            services.AddTransient<ICategoryReponsitory, CategoryReponsitory>();
            services.AddTransient<IAccountInitialize, AccountInitialize>();
            services.AddTransient<IOrderDetailReponsitory, OrderDetailReponsitory>();
            services.AddTransient<IOrderRepository, OrderReponsitory>();
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
            })
                .AddEntityFrameworkStores<QLBanHangMVC2Context>()
                .AddDefaultTokenProviders();
            services.AddAuthentication();
            services.AddAuthentication()  
            .AddFacebook(facebookOptions => {
            // Đọc cấu hình
            IConfigurationSection facebookAuthNSection = Configuration.GetSection("Authentication:Facebook");
            facebookOptions.AppId = facebookAuthNSection["AppId"];
            facebookOptions.AppSecret = facebookAuthNSection["AppSecret"];
            // Thiết lập đường dẫn Facebook chuyển hướng đến
            facebookOptions.CallbackPath = "/sigin-facebook";
            });
            services.AddMvc();
            services.AddPaging(options => {
                options.ViewName = "Bootstrap4";
                options.PageParameterName = "pageindex";
            });
            

            services.AddAuthorization(options =>
            {
                options.AddPolicy("OnlyAdmin", policy => policy.RequireRole("Admin"));
            });

            services.AddDistributedMemoryCache();           // Đăng ký dịch vụ lưu cache trong bộ nhớ (Session sẽ sử dụng nó)
            services.AddSession(cfg => {                    // Đăng ký dịch vụ Session
                cfg.Cookie.Name = "tronghieu";             // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
                cfg.IdleTimeout = new TimeSpan(0, 60, 0);    // Thời gian tồn tại của Session
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAccountInitialize accountInitialize)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSession();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            accountInitialize.SeedData();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default",
                    pattern: "{controller=Account}/{action=Login}/{int?}");
            });

        }
    }
}
