using BookStore.Infrastructure.Data;
using BookStore.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using BookStore.Infrastructure.DataAccess;

namespace BookStore.Infrastructure
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ISqlDataAccess, SqliteDataAccess>();
            services.AddDbContext<StoreContext>();

            services.AddScoped<IBookRepository, BookEfCoreRepository>();
            services.AddScoped<IAuthorRepository, AuthorEfCoreRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddSingleton<ILoggerService, LoggerService>();

            return services;
        }
    }
}
