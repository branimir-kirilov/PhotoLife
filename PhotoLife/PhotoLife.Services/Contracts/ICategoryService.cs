using System.Collections.Generic;
using PhotoLife.Models;
using PhotoLife.Models.Enums;

namespace PhotoLife.Services.Contracts
{
    public interface ICategoryService
    {
        Category GetCategoryByName(CategoryEnum categoryEnum);
        IEnumerable<Category> GetAll();
    }
}
