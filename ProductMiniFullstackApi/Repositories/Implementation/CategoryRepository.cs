using ProductMiniFullstackApi.Models.Domain;
using ProductMiniFullstackApi.Repositories.Abstract;

namespace ProductMiniFullstackApi.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DatabaseContext _ctx;
        public CategoryRepository(DatabaseContext ctx)
        {
            this._ctx = ctx;
        }
        public bool AddUpdate(Category category)
        {
            try
            {
                if (category.Id == 0)
                    _ctx.Category.Add(category);
                else
                    _ctx.Category.Update(category);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var record = GetById(id);
                if (record == null)
                    return false;
                _ctx.Category.Remove(record);
                _ctx.SaveChanges();
                return true;
            } 
            catch (Exception ex)
            {

                return false;

            }
        }

        public IEnumerable<Category> GetAll()
        {
            return _ctx.Category.ToList();
        }

        public Category GetById(int id)
        {
            return _ctx.Category.Find(id);
        }
    }
}
