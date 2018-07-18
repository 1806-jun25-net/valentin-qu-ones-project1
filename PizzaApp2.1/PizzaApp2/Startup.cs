using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaApp2.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PizzaApp2.Models;

namespace PizzaApp2
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<PizzaContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("PizzaContext")));

            services.AddDbContext<LocationsContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("LocationsContext")));

            services.AddDbContext<UserInfoContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("UserInfoContext")));

            services.AddDbContext<OrdersContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("OrdersContext")));

            services.AddDbContext<LocationAndUserContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("LocationAndUserContext")));

            services.AddDbContext<OrderHasPizzaContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("OrderHasPizzaContext")));

            services.AddDbContext<OrderContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("OrderContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
