using FluentValidation;

namespace BattAPI.App.Services.Specific.Products.JustProducts.Models
{
    public class JustProductInputValidator : AbstractValidator<JustProductInput>
    {
        public JustProductInputValidator()
        {
            RuleFor(x => x.Name)
                .Length(5, 100);

            RuleFor(x => x.Country)
                .MaximumLength(50);

            RuleFor(x => x.Price)
                .GreaterThan(0);

            RuleFor(x => x.WarrantyMonths)
                .InclusiveBetween(0, 120);
        }
    }
}
