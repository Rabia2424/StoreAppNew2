﻿using Entities.Dto;
using Entities.Models;
using Entities.RequestParameters;
using System.Xml.Serialization;

namespace Services.Contracts
{
	public interface IProductService
	{
		IEnumerable<Product> GetAllProducts(bool trackChanges);
		IEnumerable<Product> GetLastestProducts(int n, bool trackChanges);
		IQueryable<Product> GetAllProductsWithDetails(ProductRequestParameters p);
		IQueryable<Product> GetShowCaseProducts(bool trackChanges);
		Product? GetOneProduct(int id, bool trackChanges);
		void CreateProduct(ProductDtoForInsertion product);
		void UpdateOneProduct(ProductDtoForUpdate productDto);
		void DeleteOneProduct(int id);
		ProductDtoForUpdate GetOneProductForUpdate(int id, bool trackChanges);
	}
}
