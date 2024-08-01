using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using VeterinerApp.Data;
using VeterinerApp.Models;
using VeterinerApp.Models.Entity;

public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Validasyon işlemleri için servis eklendi
        services.AddControllersWithViews().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());

        // DB bağlantısı için servis eklendi
        services.AddDbContext<VeterinerContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));

        // Identity servisi eklendi
        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.Lockout.MaxFailedAccessAttempts = 3;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 8;
            options.Password.RequireLowercase = false;
            options.User.AllowedUserNameCharacters = string.Empty;
        })
        .AddEntityFrameworkStores<VeterinerContext>()
        .AddDefaultTokenProviders();

        services.AddMvc(config =>
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            config.Filters.Add(new AuthorizeFilter(policy));
        });

        //Erişim izni reddedildiğinde yönlendirilecek sayfa belirlendi
        services.ConfigureApplicationCookie(options =>
        {
            options.AccessDeniedPath = "/Account/AccessDenied"; 
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );
        });

        //var cultureInfo = new CultureInfo("tr-TR");
        //cultureInfo.NumberFormat.NumberDecimalSeparator = ",";
        //cultureInfo.NumberFormat.NumberGroupSeparator = ".";
        //var supportedCultures = new[] { cultureInfo };
        //app.UseRequestLocalization(new RequestLocalizationOptions
        //{
        //    DefaultRequestCulture = new RequestCulture(cultureInfo),
        //    SupportedCultures = supportedCultures,
        //    SupportedUICultures = supportedCultures
        //});
    }
}
