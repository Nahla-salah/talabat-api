using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared;
using Shared.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class ProductController(IServiceManager _serviceManager) : ControllerBase
    {

        //Get All Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParameters queryParameters)
        {
            var products = await _serviceManager.ProductService.GetAllProductsAsync(queryParameters);
            return Ok(products);
        }
      //  [ProducesResponseType(typeof(ProductDto),StatusCodes.Status200OK)]
         // [ProducesResponseType(typeof(),StatusCodes.Status404NotFound )]
        //  [ProducesResponseType(typeof(), StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(typeof(), StatusCodes.Status400BadRequest)]

        //Get Product By Id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        //Get All Brands
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }

        //Get All Types 
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            var types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(types);
        }
    }
}
