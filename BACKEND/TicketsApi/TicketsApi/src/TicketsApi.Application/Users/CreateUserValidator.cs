using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TicketsApi.Application.Users;
public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.PersonId).GreaterThan(0);

        RuleFor(x => x.UserName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(72);   // BCrypt solo usa los primeros 72 bytes

        RuleFor(x => x.ProfileId).GreaterThan(0);

        RuleFor(x => x.CreatedBy).GreaterThan(0);
    }
}
