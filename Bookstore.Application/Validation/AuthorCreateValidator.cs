using Bookstore.Application.Dtos;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.Validation;

public class AuthorCreateValidator : AbstractValidator<AuthorCreate>
{
    public AuthorCreateValidator()
    {
        //Die Regeln: Vorname und Nachname string Länge(max. 30) und das Buch hat keine Title (darf nicht leer sein) implementiert

        RuleFor(authorCreate => authorCreate.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(authorCreate => authorCreate.LastName).NotEmpty().MaximumLength(50);
    }
}
