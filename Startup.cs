using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
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
        //validasyon işlemleri için servis eklendi
        services.AddControllersWithViews().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());

        //DB bağlantısı için servis eklendi
        services.AddDbContext<VeterinerContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));

        //identity servisi eklendi
        services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<VeterinerContext>().AddDefaultTokenProviders();


    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();
        
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
