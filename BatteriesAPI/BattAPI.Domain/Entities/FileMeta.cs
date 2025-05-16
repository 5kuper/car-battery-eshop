namespace BattAPI.Domain.Entities
{
    public enum FileKind { Image }

    public class FileMeta : EntityBase
    {
        public required string FileName { get; set; }

        public required FileKind Kind { get; set; }

        public required string RelativePath { get; set; }

        public required long Length { get; set; }

        public required string ContentType { get; set; }
    }
}