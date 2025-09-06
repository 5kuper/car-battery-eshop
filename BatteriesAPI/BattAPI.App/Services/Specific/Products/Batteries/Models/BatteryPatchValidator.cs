using FluentValidation;
using BattAPI.Domain.ValueObj;
using BattAPI.App.Services.Specific.Products.JustProducts.Models;

namespace BattAPI.App.Services.Specific.Products.Batteries.Models
{
    public class BatteryPatchValidator : AbstractValidator<BatteryPatch>
    {
        public BatteryPatchValidator(IValidator<JustProductPatch> baseValidator, IValidator<BatterySpecs> specsValidator)
        {
            Include(baseValidator);

            RuleFor(x => x.Model)
                .MaximumLength(100);

            RuleFor(x => x.Specs!)
                .SetValidator(specsValidator)
                .When(x => x.Specs != null);

            RuleFor(x => x.Tags)
                .NotEmpty()
                .When(x => x.Tags != null);
        }
    }
}
