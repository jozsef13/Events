using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Events.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Events.Services;
using Events.Repositories;
using Events.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Events.ErrorHandling;
using Events.Areas.Identity.Data;

namespace Events
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
            services.AddMvc();
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IArtistsRepository, ArtistRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IEventSubTypeRepository, EventSubTypeRepository>();
            services.AddScoped<IEventTypeRepository, EventTypeRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<ITicketRepository, TickerRepository>();

            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IEventSubTypeService, EventSubTypeService>();
            services.AddScoped<IEventTypeService, EventTypeService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<ITicketService, TicketService>();

            services.AddIdentity<EventsUser, IdentityRole>().AddEntityFrameworkStores<EventsDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            var connection = @"Server = (localdb)\mssqllocaldb; Database = EventsDb; Trusted_Connection  =True; ConnectRetryCount = 0";
            services.AddDbContext<EventsDbContext>
                (options => options.UseSqlServer(connection));
            // code omitted for brevity
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
