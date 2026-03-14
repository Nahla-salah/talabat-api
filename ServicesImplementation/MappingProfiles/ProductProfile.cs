using AutoMapper;
using Domain.Models.ProductModule;
using Microsoft.Extensions.Options;
using Shared.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction.MappingProfiles
{
    public class ProductProfile : Profile
    {

        public ProductProfile()
        {


            CreateMap<ProductBrand, BrandDto>();

            CreateMap<ProductType, TypeDto>();




            CreateMap<Product, ProductDto>()
    .ForMember(dest => dest.BrandName, options => options.MapFrom(src => src.ProductBrand.Name))
    .ForMember(dest => dest.TypeName, options => options.MapFrom(src => src.ProductType.Name))

  .ForMember(dest => dest.PictureUrl, options => options.MapFrom(src =>
    string.IsNullOrEmpty(src.PictureUrl) ? null :
    (src.PictureUrl.Contains("https://localhost:7122/")
        ? $"http://localhost:8050/{src.PictureUrl.Replace("https://localhost:7122/", "")}"
        : $"http://localhost:8050/{src.PictureUrl}")));




        }
    }
}
