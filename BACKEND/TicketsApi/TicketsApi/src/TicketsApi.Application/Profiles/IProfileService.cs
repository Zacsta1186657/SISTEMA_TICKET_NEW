using System;
using System.Collections.Generic;
using System.Text;

namespace TicketsApi.Application.Profiles
{
    public interface IProfileService
    {
        Task<CreateProfileResponse> CreateAsync (CreateProfileRequest request, int createBy, CancellationToken ct);
        Task<IReadOnlyList<ProfileDto>> ListAsync(CancellationToken ct);
    }
}
