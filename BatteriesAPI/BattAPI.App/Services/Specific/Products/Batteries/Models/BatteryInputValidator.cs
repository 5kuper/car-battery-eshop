using FluentValidation;
using BattAPI.Domain.ValueObj;
using BattAPI.App.Services.Specific.Products.JustProducts.Models;

namespace BattAPI.App.Services.Specific.Products.Batteries.Models
{
    public class BatteryInputValidator : AbstractValidator<BatteryInput>
    {
        public BatteryInputValidator(IValidator<JustProductInput> baseValidator, IValidator<BatterySpecs> specsValidator)
        {
            Include(baseValidator);

            RuleFor(x => x.Model)
                .MaximumLength(100);

            RuleFor(x => x.Specs)
                .NotNull()
                .SetValidator(specsValidator);
        }
    }
}
