using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TicketsApi.Application.Profiles
{
    public class CreateProfileValidator : AbstractValidator<CreateProfileRequest>
    {
        public CreateProfileValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);

            RuleFor(x => x.Description)
                .MaximumLength(200)
                .When(x => !string.IsNullOrWhiteSpace(x.Description));
        }
    }
}
