using Bookstore.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Domain.Validation
{
    public class BookValidator : AbstractValidator<Book>
    {
    }
}
