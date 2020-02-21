using MeetingApp.Application.Services;
using MeetingApp.Application.Services.Contracts;
using MeetingApp.Database.Context;
using MeetingApp.Database.Repositories;
using MeetingApp.Database.Repositories.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MeetingApp.Website
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
            const string connection = @"Server=db-server;Database=MeetingApp;User=sa;Password=Your_password123;";

            services.AddControllersWithViews();
            services.AddDbContext<EfContext>(options => options.UseSqlServer(connection));

            services.AddScoped<IMeetingRoomRepository, MeetingRoomRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();

            services.AddScoped<IMeetingService, MeetingService>();
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
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=MeetingRoom}/{action=Index}/{id?}");
            });
        }
    }
}
