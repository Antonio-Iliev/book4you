using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LibrarySystem.WebClient.Models;
using LibrarySystem.Data.Context;
using LibrarySystem.Services.Services;
using LibrarySystem.Services;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract.Contracts;
using LibrarySystem.Services.Validations;

namespace LibrarySystem.WebClient
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
            services.AddDbContext<LibrarySystemContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<LibrarySystemContext>()
                .AddDefaultTokenProviders();

            // Add application services.

            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IAuthorServices, AuthorServices>();
            services.AddScoped<IBooksServices, BooksServices>();
            services.AddScoped<IGenreServices, GenreServices>();
            services.AddScoped<ITownService, TownService>();
            services.AddScoped<IUsersServices, UsersServices>();
            services.AddScoped<IValidations, CommonValidations>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

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
