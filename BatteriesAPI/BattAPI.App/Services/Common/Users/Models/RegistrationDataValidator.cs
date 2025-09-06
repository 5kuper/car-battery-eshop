using BattAPI.App.Resources;
using FluentValidation;

namespace BattAPI.App.Services.Common.Users.Models
{
    public class RegistrationDataValidator : AbstractValidator<RegistrationData>
    {
        public RegistrationDataValidator()
        {
            RuleFor(x => x.Username)
                .Length(5, 20)
                .Matches(@"^[a-zA-Z0-9_.]+$")
                .WithMessage(Strings.UsernameMatch);

            RuleFor(x => x.Password)
                .Length(8, 64)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$")
                .WithMessage(Strings.PasswordMatch);
        }
    }
}
