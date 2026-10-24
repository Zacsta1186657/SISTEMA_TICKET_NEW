using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TicketsApi.Domain.Entities;

namespace TicketsApi.Application.Profiles
{
    public class ProfileService(
        IProfileRepository repository,
        IValidator<CreateProfileRequest> validator) : IProfileService
    {
        public async Task<CreateProfileResponse> CreateAsync (CreateProfileRequest request, int createBy, CancellationToken ct)
        {

            await validator.ValidateAndThrowAsync(request, ct);

            var profile = new Profile
            {
                Name = request.Name.Trim(),
                Description = string.IsNullOrWhiteSpace(request.Description) ? null : request.Description.Trim()
            };

            var profileId = await repository.CreateAsync(profile, createBy, ct);

            return new CreateProfileResponse(profileId);
        
        }

        public Task<IReadOnlyList<ProfileDto>> ListAsync(CancellationToken ct)
            => repository.ListAsync(ct);
    }
}
