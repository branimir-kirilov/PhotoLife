using System;
using System.Collections.Generic;
using System.Linq;
using PhotoLife.Data.Contracts;
using PhotoLife.Models;
using PhotoLife.Models.Enums;
using PhotoLife.Services.Contracts;

namespace PhotoLife.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> categoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public CategoryService(IRepository<Category> categoryRepository, IUnitOfWork unitOfWork)
        {
            if (categoryRepository == null)
            {
                throw new ArgumentNullException("categoryRepository");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }

            this.categoryRepository = categoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public Category GetCategoryByName(CategoryEnum categoryEnum)
        {
            return this.categoryRepository.GetAll.FirstOrDefault(c => c.Name.Equals(categoryEnum));
        }

        public IEnumerable<Category> GetAll()
        {
            return this.categoryRepository.GetAll.ToList();
        }
    }
}
