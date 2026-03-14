using Shared;
using Shared.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IProductService
    {
        //Get all products
        Task<IEnumerable<ProductDto>> GetAllProductsAsync(ProductQueryParameters queryParameters);

        //Get product by id
        Task<ProductDto> GetProductByIdAsync(int id);
        //get all Brands
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        //get all types
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();




    }
}
