using AutoMapper;
using Domain.Models.BasketModule;
using Shared.DTO_s.BasketModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesImplementation.MappingProfiles
{
    public class BasketProfile :Profile
    {

        public BasketProfile()
        {
            CreateMap<CustomerBasket,BasketDto>().ReverseMap();
             CreateMap<BasketItem,BasketItemDto>().ReverseMap();

        }



    }
}
