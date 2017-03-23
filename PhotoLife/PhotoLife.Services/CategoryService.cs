using System;
using System.Collections.Generic;
using System.Linq;
using PhotoLife.Data.Contracts;
using PhotoLife.Factories;
using PhotoLife.Models;
using PhotoLife.Models.Enums;
using PhotoLife.Services.Contracts;

namespace PhotoLife.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> categoryRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICategoryFactory categoryFactory;

        public CategoryService(IRepository<Category> categoryRepository, IUnitOfWork unitOfWork, ICategoryFactory categoryFactory)
        {
            if (categoryRepository == null)
            {
                throw new ArgumentNullException("categoryRepository");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }

            if (categoryFactory == null)
            {
                throw new ArgumentNullException("categoryFactory");
            }

            this.categoryRepository = categoryRepository;
            this.unitOfWork = unitOfWork;
            this.categoryFactory = categoryFactory;
        }

        public Category CreateCategory(CategoryEnum categoryEnum)
        {
            var category = this.categoryFactory.CreateCategory(categoryEnum);

            this.categoryRepository.Add(category);
            this.unitOfWork.Commit();

            return category;
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
