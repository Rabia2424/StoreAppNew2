﻿using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
	public class CategoryManager : ICategoryService
	{
		private readonly IRepositoryManager _manager;

		public CategoryManager(IRepositoryManager manager)
		{
			_manager = manager;
		}

		public void CreateCategory(Category category)
		{
			_manager.Category.Create(category);
			_manager.Save();
		}

		public IEnumerable<Category> GetAllCategories(bool trackChanges)
		{
			return _manager.Category.FindAll(trackChanges);
		}
	}
}
