using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.ProductModule;
using ServicesAbstraction;
using ServicesImplementation.Specifications;
using Shared;
using Shared.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesImplementation
{
    public class ProductService(IUnitOfWork _unitOfWork ,IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
         var repo =   _unitOfWork.GetRepository<ProductBrand, int>();
         var Brands =await repo.GetAllAsync();
            var BrandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);
            return BrandsDto;

        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(ProductQueryParameters queryParameters)
        {
            var specification = new ProductWithTypeAndBrandSpecifications(queryParameters);
            var products =await _unitOfWork.GetRepository<Product, int>().GetAllAsync(specification);

            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var types =await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
          
           return _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(types);
        
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var specification = new ProductWithTypeAndBrandSpecifications(id);
            var product =await _unitOfWork.GetRepository<Product,int>().GetProductById(specification);
            if (product is null)
            {
                throw new ProductNotFoundException(id);
            }

            return _mapper.Map<Product, ProductDto>(product);
        }
    }
}
