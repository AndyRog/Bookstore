using Bookstore.Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.Validation;

public class BookSaleValidator : AbstractValidator<BookSale>
{
    public BookSaleValidator() 
    {
        RuleFor(sale => sale.Quantity).GreaterThan(0);
    }
}
