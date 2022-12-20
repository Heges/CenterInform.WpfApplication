using CenterInform.Application.Interfaces;
using CenterInform.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CenterInform.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        //public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            //var connectionString = configuration["DeffaultConnection"];
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=CenterInformDB;Trusted_Connection=True;MultipleActiveResultSets=true";
            services.AddDbContext<EmployeDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                //options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IEmployeDbContext>(provider => provider.GetService<EmployeDbContext>());
            services.AddScoped<IRepository, Repository>();
            return services;
        }
    }
}
