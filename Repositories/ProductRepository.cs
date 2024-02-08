using Entities.Models;
using Entities.RequestParameters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Repositories.Contracts;
using Repositories.Extensions;
using System.Linq;

namespace Repositories
{
	public class ProductRepository : RepositoryBase<Product>, IProductRepository
	{
		public ProductRepository(RepositoryContext context) : base(context)
		{

		}

		//public void CreateProduct(Product product) => Create(product);

		public void DeleteOneProduct(Product product)
		{
			Remove(product);
		}

		public IQueryable<Product> GetAllProducts(bool trackChanges) => FindAll(trackChanges);

		public IQueryable<Product> GetAllProductsWithDetails(ProductRequestParameters p)
		{
			return _context
				.Products
				.FilteredByCategoryId(p.CategoryId)
				.FilteredBySearchTerm(p.SearchTerm)
				//We can use filter of price like that(without using extension method)
				//.Where(prd => prd.Price >= p.MinPrice && prd.Price <= p.MaxPrice);
				//With the extension method
				.FilteredByPrice(p.MinPrice, p.MaxPrice, p.IsValidPrice)
				.ToPaginate(p.PageNumber, p.PageSize);	

				
		}

		//Interface
		public Product? GetOneProduct(int id, bool trackChanges)
		{
			return FindByCondition(c => c.ProductId == id, trackChanges);
		}

		public IQueryable<Product> GetShowCaseProducts(bool trackChanges)
		{
			return FindAll(trackChanges).Where(c => c.ShowCase == true);
		}

		public void UpdateOneProduct(Product product) => Update(product);

	}
}
