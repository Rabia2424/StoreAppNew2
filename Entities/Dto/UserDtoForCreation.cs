using System.ComponentModel.DataAnnotations;

namespace Entities.Dto
{
	public record UserDtoForCreation : UserDto
	{
		[DataType(DataType.Password)]
		[Required(ErrorMessage ="Password is required.")]
		public string? Password { get; init; }
	}
}
