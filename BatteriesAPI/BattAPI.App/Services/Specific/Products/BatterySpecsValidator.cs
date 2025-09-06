using BattAPI.Domain.ValueObj;
using FluentValidation;

namespace BattAPI.App.Services.Specific.Products
{
    public class BatterySpecsValidator : AbstractValidator<BatterySpecs>
    {
        public BatterySpecsValidator()
        {
            RuleFor(x => x.Voltage)
                .GreaterThan(0);

            RuleFor(x => x.Capacity)
                .GreaterThan(0);

            RuleFor(x => x.StartPowerRating)
                .IsInEnum();

            RuleFor(x => x.StartPower)
                .GreaterThan(0);
        }
    }
}
