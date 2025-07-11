using BattAPI.App.Common.Files;
using BattAPI.App.Common.Users;
using BattAPI.App.Specific.Products;
using BattAPI.Domain.Entities.Files;
using BattAPI.Domain.Repositories;
using BattAPI.Infra.Data.Repositories;

namespace BatteriesAPI.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBatteryService, BatteryService>();

            services.AddScoped<IFileService<FileMeta>, FileService<FileMeta>>();
            services.AddScoped<IFileService<ProductImageMeta>, FileService<ProductImageMeta>>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBatteryRepository, BatteryRepository>();

            services.AddScoped<IFileMetaRepository, FileMetaRepository>();
            services.AddScoped<IRepository<FileMeta>, FileMetaRepository>();

            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IRepository<ProductImageMeta>, ProductImageRepository>();

            return services;
        }
    }
}
