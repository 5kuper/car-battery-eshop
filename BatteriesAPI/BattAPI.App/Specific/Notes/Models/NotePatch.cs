using BattAPI.Domain.Entities;

namespace BattAPI.App.Specific.Notes.Models
{
    public class NotePatch
    {
        public string? Title { get; set; }

        public string? Content { get; set; }

        public void Apply(Note note)
        {
            if (Title != null)
                note.Title = Title;

            if (Content != null)
                note.Content = Content;
        }
    }
}
