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

			if(userDto.Roles.Count > 0)
			{
				var roleResult = await _userManager.AddToRolesAsync(user, userDto.Roles);
				if(!roleResult.Succeeded)
					throw new Exception("System have problems with roles.");
			}

			return result;
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
	}
}
