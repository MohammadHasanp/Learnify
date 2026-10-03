using BlogModule.Context;
using BlogModule.Domain;
using Common.Domain.Repository;
using Common.Infrastructure;

namespace BlogModule.Repositories.Categories
{
    interface ICategoryRepository : IBaseRepository<Category>
    {
        public void Delete(Category category);
        public List<Category> GetAll();
    }

    class CategoryRepository(BlogContext context) : BaseRepository<Category, BlogContext>(context), ICategoryRepository
    {
        public void Delete(Category category)
        {
            Context.Categories.Remove(category);
        }

        public List<Category> GetAll()
        {
            return Context.Categories.ToList();
        }
    }
}
