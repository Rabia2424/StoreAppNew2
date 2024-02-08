﻿using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Entities.Dto
{
	public record ProductDto
	{
		public int ProductId { get; init; }

		[Required(ErrorMessage = "ProductName is required.")]
		public string? ProductName { get; init; }

		[Required(ErrorMessage = "Price is required.")]
		public decimal Price { get; init; }
		public string? Summary { get; init; } = string.Empty;
		public string? ImageUrl { get; set; }
		public int? CategoryId { get; init; } 
	}
}
