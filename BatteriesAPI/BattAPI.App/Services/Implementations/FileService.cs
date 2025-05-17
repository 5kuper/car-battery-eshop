using BattAPI.App.Services.Abstractions;
using BattAPI.Domain.Entities;
using BattAPI.Domain.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BattAPI.App.Services.Implementations
{
    public class FileService(IWebHostEnvironment env, IFileMetaRepository fileMetaRepo) : IFileService
    {
        private const string BaseUploadsFolder = "uploads";

        private string WebRoot => env.WebRootPath ?? Path.Combine(env.ContentRootPath, "wwwroot");

        public async Task<FileMeta?> GetFileMetaAsync(Guid id)
        {
            return await fileMetaRepo.GetAsync(id);
        }

        public async Task<FileMeta> SaveImageFileAsync(IFormFile file, string folder)
        {
            if (file.Length == 0)
                throw new ArgumentException("File is empty.", nameof(file));

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
                throw new ArgumentException("File format is not allowed.", nameof(file));

            return await SaveFileAsync(file, folder, extension);
        }

        public async Task DeleteFileAsync(Guid metaId)
        {
            var meta = await GetFileMetaAsync(metaId)
                ?? throw new ArgumentException("File meta not found.", nameof(metaId));

            var path = Path.Combine(WebRoot, meta.RelativePath);
            File.Delete(path);

            fileMetaRepo.Remove(meta);
            await fileMetaRepo.SaveChangesAsync();
        }

        private async Task<FileMeta> SaveFileAsync(IFormFile file, string folder, string ext)
        {
            var dir = Path.Combine(WebRoot, BaseUploadsFolder, folder);

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var uniqueFileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(dir, uniqueFileName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var meta = new FileMeta
            {
                FileName = file.FileName,
                Kind = FileKind.Image,
                RelativePath = $"{BaseUploadsFolder}/{folder}/{uniqueFileName}",
                Length = file.Length,
                ContentType = file.ContentType
            };

            await fileMetaRepo.AddAsync(meta);
            await fileMetaRepo.SaveChangesAsync();

            return meta;
        }
    }
}
