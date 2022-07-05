using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Models;

namespace Products.Repositories.ProductRepository
{
    public class ProductsRepository:IProductRepository
    {
        private AppDbContext _context;

        public ProductsRepository(AppDbContext context)
        {
            _context = context;
        }

        private async Task<bool> SaveChangeAsync()=>await _context.SaveChangesAsync() > 0;
        public async Task<bool> AddProduct(Product product)
        {
            var result=await _context.Products.AddAsync(product);
            return await SaveChangeAsync();
        }
        
        public Product? FindProductById(int id)
        {
            return  _context.Products?.AsQueryable().Where(t=>t.Id==id)?.FirstOrDefault();
        }
        public IQueryable<Product>? GetAllProducts()
        {            
            return _context.Products?.AsQueryable();
        }
    }
}