using ProductMiniFullstackApi.Models.Domain;
using ProductMiniFullstackApi.Repositories.Abstract;

namespace ProductMiniFullstackApi.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseContext _ctx;
        public ProductRepository(DatabaseContext ctx)
        {
            this._ctx = ctx;
        }
        public bool AddUpdate(Product product)
        {
            try
            {
                if (product.Id == 0)
                    _ctx.Product.Add(product);
                else
                    _ctx.Product.Update(product);
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
                _ctx.Product.Remove(record);
                _ctx.SaveChanges();
                return true;
            } 
            catch (Exception ex)
            {

                return false;

            }
        }

        public IEnumerable<Product> GetAll(string term = "")
        {
            term = term.ToLower();
            var data = (from product in _ctx.Product
                        join
                        category in _ctx.Category
                        on product.CategoryId equals category.Id
                        where term=="" || product.Name.ToLower().StartsWith(term)
                        select new Product
                        {
                            Id = product.Id,
                            CategoryId = product.CategoryId,
                            Name = product.Name,
                            CategoryName = category.Name,
                            Price = product.Price
                        }
                        ).ToList();
            return data;
        }

        public Product GetById(int id)
        {
            return _ctx.Product.Find(id);
        }
    }
}
