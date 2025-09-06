using FluentValidation;

namespace BattAPI.App.Services.Specific.Products.JustProducts.Models
{
    public class JustProductPatchValidator : AbstractValidator<JustProductPatch>
    {
        public JustProductPatchValidator()
        {
            RuleFor(x => x.Name)
                .Length(5, 100);

            RuleFor(x => x.Country)
                .MaximumLength(50);

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .When(x => x.Price.HasValue);

            RuleFor(x => x.WarrantyMonths)
                .InclusiveBetween(0, 120)
                .When(x => x.WarrantyMonths.HasValue);
        }
    }
}
