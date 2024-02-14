namespace Entities.Dto
{
	public record ProductDtoForUpdate : ProductDto
	{
		public bool Showcase {  get; set; }	
	}
}
