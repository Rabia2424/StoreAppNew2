using Entities.Dto;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts
{
	public interface IAuthService
	{
		IEnumerable<IdentityRole> GetAllRoles { get; }
		void CreateRole(IdentityRole role);
		void DeleteRole(IdentityRole role);
		IEnumerable<IdentityUser> GetAllUsers();

		Task<IdentityResult> CreateUser(UserDtoForCreation userDto);
		Task<IdentityUser> GetOneUser(string userName);
		Task<UserDtoForUpdate> GetOneUserForUpdate(string userName);
		Task Update(UserDtoForUpdate userDto);
		
	}
}
