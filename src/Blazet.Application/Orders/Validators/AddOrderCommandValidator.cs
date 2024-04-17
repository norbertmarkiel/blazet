using Blazet.Application.Orders.Commands;
using FluentValidation;

namespace Blazet.Application.Orders.Validators
{
    public class AddOrderCommandValidator : AbstractValidator<AddOrderCommand>
    {
        public AddOrderCommandValidator()
        {
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Field is required")
                .GreaterThan(0).WithMessage("Value is too low");
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Field is required")
                .GreaterThan(0).WithMessage("Value is too low");
        }
    }
}
