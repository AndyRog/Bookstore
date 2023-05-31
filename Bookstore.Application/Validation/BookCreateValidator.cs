using Bookstore.Application.Dtos;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.Validation;

public class BookCreateValidator : AbstractValidator<BookCreate>
{
    public BookCreateValidator()
    {
        //Die Regeln: Title string Länge(max. 100) und das Buch hat keine Title (darf nicht leer sein) implementiert

        RuleFor(bookCreate => bookCreate.Title).NotEmpty().MaximumLength(100);
    }
}
