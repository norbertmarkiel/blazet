using Blazet.Application.Orders.DTOs;
using FluentValidation;

namespace Blazet.WebApp.Components.Pages.Orders.Validators
{
    public class OrderDtoValidator : AbstractValidator<OrderDto>
    {
        public OrderDtoValidator()
        {
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Field is required")
                .GreaterThan(0).WithMessage("Value is too low");
        }
    }
}
