using Bookstore.DBContext;
using Bookstore.Models;
using Bookstore.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace Bookstore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDBContext>(
                option => option.UseSqlServer(builder.Configuration.GetConnectionString("constr"))
            );
            builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
            builder.Services.AddIdentity<Models.Customer, IdentityRole>(options =>
            {
                
            })
                 .AddEntityFrameworkStores<AppDBContext>();
            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
            var app = builder.Build();

            StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe")["SecretKey"];
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
