using HouseRentingSystem.Core.Contracts.Agent;
using HouseRentingSystem.Core.Contracts.House;
using HouseRentingSystem.Core.Services.Agent;
using HouseRentingSystem.Core.Services.House;
using HouseRentingSystem.Infrastructure.Data;
using HouseRentingSystem.Infrastructure.Data.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddAppService(this IServiceCollection services)
        {
            services.AddScoped<IHouseService, HouseService>();
            services.AddScoped<IAgentService, AgentService>();

            return services;
        }

        public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<HouseRentingSystemDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IRepository, Repository>();

            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }

        public static IServiceCollection AddAppIdentity(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<HouseRentingSystemDbContext>();

            return services;
        }
    }
}
