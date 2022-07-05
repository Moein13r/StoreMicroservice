using Products.Models;

namespace Products.Repositories.ProductRepository
{
    public interface IProductRepository
    {         
        Task<bool> AddProduct(Product product);
        Product FindProductById(int id);
        IQueryable<Product> GetAllProducts();
    }
}