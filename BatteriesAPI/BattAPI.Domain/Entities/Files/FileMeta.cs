namespace BattAPI.Domain.Entities.Files
{
    public enum FileKind { Image }

    public class FileMeta : Entity
    {
        public string FileName { get; set; } = null!;

        public FileKind Kind { get; set; }

        public string RelativePath { get; set; } = null!;

        public long Length { get; set; }

        public string ContentType { get; set; } = null!;
    }
}