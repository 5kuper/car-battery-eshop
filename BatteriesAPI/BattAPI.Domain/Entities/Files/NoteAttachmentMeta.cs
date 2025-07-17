namespace BattAPI.Domain.Entities.Files
{
    public class NoteAttachmentMeta : FileMeta
    {
        public Guid NoteId { get; set; }

        public virtual Note Note { get; set; } = null!;
    }
}
