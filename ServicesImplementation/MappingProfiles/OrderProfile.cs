using AutoMapper;
using Domain.Models.OrderModule;
using Shared.DTO_s.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesImplementation.MappingProfiles
{
    public class OrderProfile :Profile
    {

        public OrderProfile()
        {
            CreateMap<AddressDto, OrderAddress>();
            CreateMap<OrderAddress, AddressDto>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.GetTotal()));
         
            CreateMap<OrderItem,OrderItemDto>()
             
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ItemOrdered.ProductName))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.ItemOrdered.PictureUrl));

            CreateMap<DeliveryMethod, DeliveryMethodResult>();
        }
    }
}

