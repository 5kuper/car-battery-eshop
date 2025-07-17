using BattAPI.Domain.Entities.Files;

namespace BattAPI.App.Specific.Notes.Models
{
    public class NoteDto
    {
        public record Attachment(Guid Id, string Url, FileKind Kind);


        public required string Title { get; set; }

        public required string Content { get; set; }

        public required IList<Attachment> Attachments { get; set; }
    }
}
