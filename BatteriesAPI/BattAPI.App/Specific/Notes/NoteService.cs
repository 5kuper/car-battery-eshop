using AutoMapper;
using BattAPI.App.Common.Files;
using BattAPI.App.Specific.Notes.Models;
using BattAPI.App.Utils;
using BattAPI.Domain.Entities;
using BattAPI.Domain.Entities.Files;
using BattAPI.Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace BattAPI.App.Specific.Notes
{
    public class NoteService(INoteRepository repo, IMapper mapper, IFileService<NoteAttachmentMeta> fileService)
        : DtoServiceBase<Note, INoteRepository, NoteInput, NoteDto, NotePatch>(repo, mapper), INoteService
    {
        private const string AttachmentUploadFolder = "notes";

        public async Task<NoteAttachmentMeta> CreateImageAttachment(Guid noteId, IFormFile image)
        {
            var note = Repository.GetAsync(noteId)
                ?? throw new ArgumentException("Note not found.", nameof(noteId));

            return await fileService.SaveImageFileAsync(image, AttachmentUploadFolder, meta => meta.NoteId = noteId);
        }

        public async Task DeleteAttachment(Guid attachmentId)
        {
            await fileService.DeleteFileAsync(attachmentId);
        }

        protected override void Patch(Note entity, NotePatch patch) => patch.Apply(entity);
    }
}
