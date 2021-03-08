using BookStore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        //"set" method added so we can change the configuration
        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //this service is added, connection to the connection string that we created in DBContext class and the BookStoreConnection string connection created in appsettings.json
            services.AddDbContext<BookDBContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:BookStoreConnection"]);
            });

            //this is a service added, for each sesson will get its own repository objec
            services.AddScoped<IBookRepository, EFBookRepository>();


            //add razor pages to project
            services.AddRazorPages();

            //servcies that will allow the cart to keep its memory
            services.AddDistributedMemoryCache();
            services.AddSession();

            //adding stuff from chapter 9
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //build a session for the user to keep the cart intact
            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            
            app.UseEndpoints(endpoints =>
            {
                //endpoint for category/classification
                //user can specify the classification of the book, and the page number their desire within that category
                endpoints.MapControllerRoute("classificationpage",
                    "{classification}/{pageNum:int}",
                    new { Controller = "Home", action = "Index" });

                // only page given endpoints
                endpoints.MapControllerRoute("page",
                    "Books/{pageNum:int}",
                    new { Controller = "Home", action = "Index" });

                //only classification given enpoints, set page to 1 if user does not specifiy page
                endpoints.MapControllerRoute("classification",
                    "{classification}",
                    new { Controller = "Home", action = "Index" , pageNum = 1});

                //endpoint for page number, this is similar to the other page one but with a different format option for the user
                //could be in "Books/P{page}" for URL, but I am choosing to use JUST /P{page} in order to access page
                endpoints.MapControllerRoute(
                    "pagination",
                    "/P{pageNum}",
                    new { Controller = "Home", action = "Index" });

                //default endpoint, if user does not specify anything in the url
                endpoints.MapDefaultControllerRoute();

                //enpoint to map razor pages
                endpoints.MapRazorPages();
            });

            //call method
            SeedData.EnsurePopulated(app);
        }
    }
}
