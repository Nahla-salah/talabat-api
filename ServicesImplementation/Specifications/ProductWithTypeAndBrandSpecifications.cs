using Domain.Models.ProductModule;
using Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesImplementation.Specifications
{
    public class ProductWithTypeAndBrandSpecifications : BaseSpecifications<Product, int>
    {

        public ProductWithTypeAndBrandSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }



        public ProductWithTypeAndBrandSpecifications(ProductQueryParameters queryParameters)
            : base(p =>
            (!queryParameters.brandId.HasValue || p.BrandId == queryParameters.brandId.Value) &&
            (!queryParameters.typeId.HasValue || p.TypeId == queryParameters.typeId.Value)&&
            (string.IsNullOrEmpty(queryParameters.search) || p.Name.ToLower().Contains(queryParameters.search.ToLower()))


            )




        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

            switch (queryParameters.sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    break;
            }




        }




    }
}

   


