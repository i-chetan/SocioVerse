using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocioVerse.Application.Interfaces;
using SocioVerse.Infrastructure.Persistence;
using SocioVerse.Infrastructure.Repositorires;
using SocioVerse.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SocioVerse.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<SocialMediaDbContext>(options => options.UseSqlServer(config["DefaultConnection"]));

            services.AddScoped<IUserService, UserRepository>();
            services.AddScoped<IPostService,  PostRepository>();

            services.Configure<JWTOptions>(config.GetSection(JWTOptions.Section));

            services.AddScoped<IJWTService, JWTService>();

            return services;
        }
    }
}
