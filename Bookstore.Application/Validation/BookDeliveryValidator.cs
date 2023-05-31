using Bookstore.Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.Validation;

public class BookDeliveryValidator : AbstractValidator<BookDelivery>
{
    public BookDeliveryValidator()
    {
        RuleFor(delivery => delivery.Quantity).GreaterThanOrEqualTo(0);
    }
}
