
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResponseService.Services
{
    public interface ICatalogService
    {
        Product GetProductBySlug(string id);
        List<Product> GetAllProducts();
    }
}