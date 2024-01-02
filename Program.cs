using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WebApplication2.Areas.Identity.Pages.Account;
using WebApplication2.Class_Files;
using WebApplication2.Data;
using WebApplication2.Model;
using WebApplication2.Pages;
using WebApplication2.Repository;

namespace WebApplication2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddScoped<InterfaceUpload, UploadDetails>(); 
                 builder.Services.AddScoped<InterfaceLoanTwo, LoanUploadTwo>(); 
                 builder.Services.AddScoped<InterfaceCashflows, CashFlow>(); 
                builder.Services.AddScoped<InterfaceDaily, Daily>(); 
                builder.Services.AddScoped<InterfaceCoupons, CouponUploads>();
           
                builder.Services.AddScoped<InterfaceState, StateUploads>(); 
                builder.Services.AddScoped<InterfaceTBill, TBillUploads>();
                builder.Services.AddTransient<GenerateExcelBR>();

               builder.Services.AddTransient<ParameterCalls>();
            
            
            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddTransient<IEmailSender, SmtpEmailSender>();

            //builder.Services.Configure<SmtpEmailSenderOptions>(builder.Configuration.GetSection("SmtpEmailSender"));
            //builder.Services.AddSingleton<SmtpEmailSenderOptions>();
            //builder.Services.AddSingleton<IEmailSender, SmtpEmailSender>();



            //builder.Services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(15); 
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.IsEssential = true;
            //});
            //builder.Services.AddScoped<TimerService>();

            builder.Services.AddRazorPages();

            builder.Services.AddDistributedMemoryCache();

            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseExceptionHandler("/Error");
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //using (var scope = app.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;

            //    // Resolve TimerServiceModel from the service provider
            //    var timerService = services.GetRequiredService<TimerService>();

            //    // Start the timer
            //    timerService.StartTimer();
            //}

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseSession();
            app.MapRazorPages();

            app.Run();
        }
    }
}