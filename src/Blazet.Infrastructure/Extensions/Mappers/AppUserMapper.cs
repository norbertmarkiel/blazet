using Blazet.Application.Identity.DTOs;
using Blazet.Domain.Identity;

namespace Blazet.Infrastructure.Extensions.Mappers
{
    public static class AppUserMapper
    {
        public static AppUserDto AsDto(this AppUser entity)
        {
            return new AppUserDto
            {
                Id = entity.Id,
                UserName = entity.UserName ?? throw new NullReferenceException("The value of 'entity.UserName' should not be null"),
                DisplayName = entity.DisplayName,
                Provider = entity.Provider,
                Email = entity.Email ?? throw new NullReferenceException("The value of 'entity.Email' should not be null"),
                PhoneNumber = entity.PhoneNumber,
                IsActive = entity.IsActive,
                LockoutEnd = entity.LockoutEnd
            };
        }
    }
}
