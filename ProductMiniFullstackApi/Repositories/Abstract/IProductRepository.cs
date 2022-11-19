using ProductMiniFullstackApi.Models.Domain;

namespace ProductMiniFullstackApi.Repositories.Abstract
{
    public interface IProductRepository
    {
        bool AddUpdate(Product Product);
        bool Delete(int id);
        Product GetById(int id);
        IEnumerable<Product> GetAll(string term="");
    }
}
