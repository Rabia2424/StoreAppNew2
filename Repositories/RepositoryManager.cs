using Repositories.Contracts;

namespace Repositories
{
	public class RepositoryManager : Contracts.IRepositoryManager
	{
		private readonly RepositoryContext _context;

		private readonly IProductRepository _productRepository;

		private readonly ICategoryRepository _categoryRepository;	

		private readonly IOrderRepository _orderRepository;

		public RepositoryManager(IProductRepository productRepository, RepositoryContext context, ICategoryRepository categoryRepository,IOrderRepository orderRepository)
		{
			_productRepository = productRepository;
			_categoryRepository = categoryRepository;
			_orderRepository = orderRepository;	
			_context = context;
		}
		
		public IProductRepository Product => _productRepository;

		public ICategoryRepository Category => _categoryRepository;

		public IOrderRepository Order => _orderRepository;

		public void Save()
		{
			_context.SaveChanges();
		}
	}
}
