using BattAPI.App.Specific.Notes.Models;
using BattAPI.App.Utils;
using BattAPI.Domain.Entities.Files;
using Microsoft.AspNetCore.Http;

namespace BattAPI.App.Specific.Notes
{
    public interface INoteService : IDtoServiceBase<NoteInput, NoteDto, NotePatch>
    {
        Task<NoteAttachmentMeta> CreateImageAttachment(Guid noteId, IFormFile image);

        Task DeleteAttachment(Guid attachmentId);
    }
}
