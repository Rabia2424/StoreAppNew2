using System.ComponentModel.DataAnnotations;

namespace Entities.Dto
{
	public record RegisterDto
	{
		[Required(ErrorMessage ="UserName is required.")]
		public string? UserName { get; init; }

		[Required(ErrorMessage = "Email is required.")]
		public string? Email { get; init; }

		[Required(ErrorMessage = "Password is required.")]
		public string? Password { get; init; }
	}
}
