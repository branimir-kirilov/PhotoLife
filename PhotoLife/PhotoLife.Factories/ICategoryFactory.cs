using PhotoLife.Models;
using PhotoLife.Models.Enums;

namespace PhotoLife.Factories
{
    public interface ICategoryFactory
    {
        Category CreateCategory(CategoryEnum name);
    }
}
