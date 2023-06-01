using Bookstore.Application.Dtos;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.Validation;

public class AuthorUpdateValidator : AbstractValidator<AuthorUpdate>
{
    public AuthorUpdateValidator()
    {
        //Die Regeln: Vorname und Nachname string Länge(max. 50) und das Buch hat keine Title (darf nicht leer sein) implementiert

        RuleFor(authorUpdate => authorUpdate.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(authorUpdate => authorUpdate.LastName).NotEmpty().MaximumLength(50);
    }
}
