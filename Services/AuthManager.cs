using AutoMapper;
using Entities.Dto;
using Microsoft.AspNetCore.Identity;
using Services.Contracts;

namespace Services
{
	public class AuthManager : IAuthService
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IMapper _mapper;

		public AuthManager(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IMapper mapper)
		{
			_roleManager = roleManager;
			_userManager = userManager;
			_mapper = mapper;
		}

		public IEnumerable<IdentityRole> GetAllRoles =>
			_roleManager.Roles;

		public void CreateRole(IdentityRole role)
		{
			_roleManager.CreateAsync(role).Wait();
		}

		public async Task<IdentityResult> CreateUser(UserDtoForCreation userDto)
		{
			IdentityUser user = _mapper.Map<IdentityUser>(userDto);
			var result = await _userManager.CreateAsync(user, userDto.Password);

			if (!result.Succeeded)
				throw new Exception("User could not be created.");

			if (userDto.Roles.Count > 0)
			{
				var roleResult = await _userManager.AddToRolesAsync(user, userDto.Roles);
				if (!roleResult.Succeeded)
					throw new Exception("System have problems with roles.");
			}

			return result;
		}

		public async Task<IdentityResult> DeleteOneUser(string userName)
		{
			var user = await GetOneUser(userName);
			return await _userManager.DeleteAsync(user);
		}

		public void DeleteRole(IdentityRole role)
		{
			_roleManager.DeleteAsync(role).Wait();
		}

		public IEnumerable<IdentityUser> GetAllUsers()
		{
			List<IdentityUser> users = _userManager.Users.ToList();
			return users;
		}

		public async Task<IdentityUser> GetOneUser(string userName)
		{
			IdentityUser user = await _userManager.FindByNameAsync(userName);
			if (user is not null)
				return user;
			throw new Exception("User could not be found.");
		}

		public async Task<UserDtoForUpdate> GetOneUserForUpdate(string userName)
		{
			var user = await GetOneUser(userName);

			UserDtoForUpdate userDto = _mapper.Map<UserDtoForUpdate>(user);
			userDto.Roles = new HashSet<string>(GetAllRoles.Select(r => r.Name).ToList());
			userDto.UserRoles = new HashSet<string>(await _userManager.GetRolesAsync(user));
			return userDto;

		}

		public async Task<IdentityResult> ResetPassword(ResetPasswordDto model)
		{
			var user = await GetOneUser(model.UserName);

			await _userManager.RemovePasswordAsync(user);
			var result = await _userManager.AddPasswordAsync(user, model.Password);
			return result;
		}

		public async Task Update(UserDtoForUpdate userDto)
		{
			IdentityUser user = await _userManager.FindByNameAsync(userDto.UserName);
			user.PhoneNumber = userDto.PhoneNumber;
			user.Email = userDto.Email;

			if (user is not null)
			{
				var result = await _userManager.UpdateAsync(user);
				if (userDto.Roles.Count > 0)
				{
					var userRoles = await _userManager.GetRolesAsync(user);
					var r1 = await _userManager.RemoveFromRolesAsync(user, userRoles);
					var r2 = await _userManager.AddToRolesAsync(user, userDto.Roles);
				}
				return;
			}
			throw new Exception("System has problem with user update.");
		}
	}
}
