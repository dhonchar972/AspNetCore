using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStore.Models;

namespace SportsStore;

public class Startup
{
    // DI!!!
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    // DI!!!

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]));

        services.AddDbContext<AppIdentityDbContext>(options =>
            options.UseSqlServer(Configuration["Data:SportStoreIdentity:ConnectionString"]));

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();

        services.AddTransient<IProductRepository, EFProductRepository>();
        services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<IOrderRepository, EFOrderRepository>();
        services.AddMvc(options => options.EnableEndpointRouting = false);
        services.AddMemoryCache();
        services.AddSession();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
        }
        else
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseStaticFiles();
        app.UseSession();
        app.UseAuthentication();
        app.UseMvc(routes =>
        {
            routes.MapRoute(name: "Error", template: "Error",
                defaults: new { controller = "Error", action = "Error" });

            routes.MapRoute(
                name: null,
                template: "{category}/Page{productPage:int}",
                defaults: new { controller = "Product", action = "List" }
            );

            routes.MapRoute(
                name: null,
                template: "Page{productPage:int}",
                defaults: new { controller = "Product", action = "List", productPage = 1 }
            );

            routes.MapRoute(
                name: null,
                template: "{category}",
                defaults: new { controller = "Product", action = "List", productPage = 1 }
            );

            routes.MapRoute(
                name: null,
                template: "",
                defaults: new { controller = "Product", action = "List", productPage = 1 });

            routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
        });

        //SeedData.EnsurePopulated(app);
        //IdentitySeedData.EnsurePopulated(app);
    }
}