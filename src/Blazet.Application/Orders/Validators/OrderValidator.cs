﻿using Blazet.Domain.Orders.Entities;
using FluentValidation;

namespace Blazet.Application.Orders.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
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