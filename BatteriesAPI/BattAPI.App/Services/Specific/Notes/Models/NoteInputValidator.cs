using FluentValidation;

namespace BattAPI.App.Services.Specific.Notes.Models
{
    public class NoteInputValidator : AbstractValidator<NoteInput>
    {
        public NoteInputValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(5000);
        }
    }
}
