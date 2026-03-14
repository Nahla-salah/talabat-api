using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.BasketModule;
using ServicesAbstraction;
using Shared.DTO_s.BasketModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesImplementation
{
    public class BasketService(IBasketRepository basketRepository,IMapper mapper) : IBasketService
    {

        public async Task<BasketDto> GetBasketAsync(string id)
        {
            var Basket = await basketRepository.GetBasketAsync(id);
            if (Basket != null)
           return     mapper.Map<CustomerBasket, BasketDto>(Basket);
            else
                throw new BasketNotFoundException(id);


                    }

        
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var Basket = mapper.Map<BasketDto, CustomerBasket>(basket);
            // Specify a TimeSpan value for the required parameter, e.g., 30 minutes
            var CreatedBasket = await basketRepository.CreateOrUpdateBasketAsync(Basket, TimeSpan.FromMinutes(30));
            if (CreatedBasket != null)
                return await GetBasketAsync(Basket.Id);
            else
                throw new Exception("Problem Creating Or Updating The Basket");
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await basketRepository.DeleteBasketAsync(id);
        }
    }
}
