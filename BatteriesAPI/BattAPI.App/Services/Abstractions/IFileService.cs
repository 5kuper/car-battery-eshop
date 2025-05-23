﻿using BattAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace BattAPI.App.Services.Abstractions
{
    public interface IFileService
    {
        Task<FileMeta?> GetFileMetaAsync(Guid id);

        Task<FileMeta> SaveImageFileAsync(IFormFile file, string folder);

        Task DeleteFileAsync(Guid metaId);
    }
}
