using BattAPI.Domain.Entities.Files;

namespace BattAPI.Domain.Entities
{
    public class Note : Entity
    {
        public required string Title { get; set; }

        public required string Content { get; set; }

        public required IList<NoteAttachmentMeta> Attachments { get; set; }
    }
}
