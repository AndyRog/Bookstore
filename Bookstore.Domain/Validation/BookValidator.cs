using Bookstore.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Domain.Validation;

public class BookValidator : AbstractValidator<Book>
{
    public BookValidator() 
    {
        RuleFor(book => book.Isbn).Length(13);
        RuleFor(book => book.Quantity).GreaterThanOrEqualTo(0);
        RuleFor(book => book.Author).NotNull();
    }  
}
