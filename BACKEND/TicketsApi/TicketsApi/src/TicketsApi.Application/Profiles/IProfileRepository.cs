using System;
using System.Collections.Generic;
using System.Text;
using TicketsApi.Domain.Entities;

namespace TicketsApi.Application.Profiles
{
    public interface IProfileRepository
    {
        /// <summary>Inserta el perfil y devuelve su ProfileId.</summary>
        /// <exception cref="Exceptions.ConflictException">Ya existe un perfil con ese nombre.</exception>
        /// <exception cref="Exceptions.ReferenceNotFoundException">createdBy no existe.</exception>
        Task<int> CreateAsync(Profile profile, int createdBy, CancellationToken ct);

        Task<IReadOnlyList<ProfileDto>> ListAsync(CancellationToken ct);
    }
}
