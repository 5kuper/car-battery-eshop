using BattAPI.Domain.Entities.Files;
using Microsoft.AspNetCore.Http;

namespace BattAPI.App.Services.Common.Files
{
    public interface IFileService<TMeta> where TMeta : FileMeta
    {
        Task<TMeta?> GetFileMetaAsync(Guid id);

        Task DeleteFileAsync(Guid metaId);


        Task<TMeta> SaveImageFileAsync(IFormFile file, string folder, Action<TMeta>? configureMeta = null);
    }
}
