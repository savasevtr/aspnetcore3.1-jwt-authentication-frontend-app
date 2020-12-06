using System.Collections.Generic;
using System.Threading.Tasks;
using UdemyJwtProjectFrontend.Models;

namespace UdemyJwtProjectFrontend.ApiServices.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductList>> GetAllAsync();
        Task AddAsync(ProductAdd productList);
        Task<ProductList> GetByIdAsync(int id);
        Task UpdateAsync(ProductList productList);
        Task DeleteAsync(int id);
    }
}