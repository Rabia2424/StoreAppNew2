using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace StoreAppNew2.Infrastructure.Extensions
{
	public static class ApplicationExtension
	{
		public static void ConfigureAndCheckMigration(this IApplicationBuilder app)
		{
			RepositoryContext context = app
				.ApplicationServices
				.CreateScope()
				.ServiceProvider
				.GetRequiredService<RepositoryContext>();

			if (context.Database.GetPendingMigrations().Any())
			{
				context.Database.Migrate();
			}
		}

		public static async void ConfigureDefaultAdminUser(this IApplicationBuilder app)
		{
			const string adminUser = "Admin";
			const string adminPassword = "Admin+123456";

			//UserManager
			UserManager<IdentityUser> userManager = app
				.ApplicationServices
				.CreateScope()
				.ServiceProvider
				.GetRequiredService<UserManager<IdentityUser>>();
			//RoleManager
			RoleManager<IdentityRole> roleManager = app
				.ApplicationServices
				.CreateAsyncScope()
				.ServiceProvider
				.GetRequiredService<RoleManager<IdentityRole>>();

			IdentityUser user = await userManager.FindByNameAsync(adminUser);
			if (user is null)
			{
				user = new IdentityUser()
				{
					Email = "admin24@gmail.com",
					PhoneNumber = "5061112233",
					UserName = adminUser,
				};

				var result = await userManager.CreateAsync(user, adminPassword);
				if (!result.Succeeded)
				{
					throw new Exception("Admin user could not created.");
				}


				var roleResult = await userManager.AddToRolesAsync(user, 
					roleManager
					.Roles
					.Select(r => r.Name)
					.ToList()
				);
				//I can define like below here but every role added to application I have to add here too so that I'll use dynamic codes for that structure.
				//new List<string>() 
				//{ 
				//	"Admin",
				//	"Editor",
				//	"User"
				//}
				if (!roleResult.Succeeded)
					throw new Exception("System have problems with role defination for admin.");
			}
		}

	}
}
