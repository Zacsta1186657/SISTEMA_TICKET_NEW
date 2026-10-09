using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TicketsApi.Application.Security;
using TicketsApi.Domain.Entities;

namespace TicketsApi.Application.Users
{
    public class UserService(
        IUserRepository userRepository,
        IValidator<CreateUserRequest> validator,
        IPasswordService passwords) : IUserService
    {
        public async Task<CreateUserResponse> CreateAsync(CreateUserRequest request, CancellationToken ct)
        {
            await validator.ValidateAndThrowAsync(request, ct);

            var user = new User
            {
                PersonId = request.PersonId,
                UserName = request.UserName.Trim(),
                PasswordHash = passwords.Hash(request.Password)
            };

            var userId = await userRepository.CreateAsync(user, request.ProfileId, request.CreatedBy, ct);
            return new CreateUserResponse(userId);
        }
    }
}
