using BankTransactions.Models;
using Microsoft.EntityFrameworkCore;

namespace BankTransactions {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<TransactionDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=TransactionsCruds}/{action=Index}/{id?}");

            app.Run();


            //www.youtube.com/watch?v=VYmsoCWjvM4&t=19s&ab_channel=CodAffection 18:47
        }
    }
}