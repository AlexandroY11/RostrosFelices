using Microsoft.EntityFrameworkCore;
using RostrosFelices._Repositories;
using RostrosFelices.Data;

namespace RostrosFelices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<RostrosFelicesContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("RostrosFelicesDB"))
            );

			builder.Services.AddAuthentication().AddCookie("CookieAuth", options =>
			{
				options.Cookie.Name = "CookieAuth";
				options.LoginPath = "/Account/Login";
			});

			builder.Services.AddScoped<UserRepository>();

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}