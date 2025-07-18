using BattAPI.App.Common.Files;
using BattAPI.App.Common.Users;
using BattAPI.App.Specific.Notes;
using BattAPI.App.Specific.Products;
using BattAPI.App.Specific.Products.Batteries;
using BattAPI.App.Specific.Products.JustProducts;
using BattAPI.Domain.Entities.Files;
using BattAPI.Domain.Repositories;
using BattAPI.Infra.Data.Repositories;
using BattAPI.Infra.Data.Repositories.Files;

namespace BatteriesAPI.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<INoteService, NoteService>();

            services.AddScoped<IGeneralProductService, GeneralProductService>();
            services.AddScoped<IJustProductService, JustProductService>();
            services.AddScoped<IBatteryService, BatteryService>();

            services.AddScoped<IFileService<FileMeta>, FileService<FileMeta>>();
            services.AddScoped<IFileService<ProductImageMeta>, FileService<ProductImageMeta>>();
            services.AddScoped<IFileService<NoteAttachmentMeta>, FileService<NoteAttachmentMeta>>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBatteryRepository, BatteryRepository>();

            services.AddScoped<IFileMetaRepository, FileMetaRepository>();
            services.AddScoped<IRepository<FileMeta>, FileMetaRepository>();

            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IRepository<ProductImageMeta>, ProductImageRepository>();
            services.AddScoped<INoteAttachmentRepository, NoteAttachmentRepository>();
            services.AddScoped<IRepository<NoteAttachmentMeta>, NoteAttachmentRepository>();

            return services;
        }
    }
}
