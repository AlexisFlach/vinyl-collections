using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using VinylCollection.Core;
using VinylCollection.Mediator;
using VinylCollection.Persistence;
using VinylCollection.Repositories;

namespace VinylCollection.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {   

            services.AddSingleton<IMemVinylsRepository, InMemVinylsRepository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VinylCollection", Version = "v1" });
            });
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            services.AddMediatR(typeof(List.Handler).Assembly);
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            return services;
        }
    }
}