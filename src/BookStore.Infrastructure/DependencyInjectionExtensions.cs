using BookStore.Infrastructure.Data;
using BookStore.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using BookStore.Infrastructure.DataAccess;
using BookStore.Infrastructure.Identity;
using BookStore.Infrastructure.Services;

namespace BookStore.Infrastructure
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ISqlDataAccess, SqliteDataAccess>();

            services.AddDbContext<ApplicationDbContext>();
            services.AddDbContext<AppIdentityDbContext>();
            
            services.AddMemoryCache();
            services.AddSingleton<IBookCache, BookCache>();
            services.AddSingleton<IAuthorCache, AuthorCache>();

            services.AddScoped<IBookRepository, BookSqlRepository>();
            services.AddScoped<IAuthorRepository, AuthorSqlRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<ILoggerService, LoggerService>();

            return services;
        }
    }
}
