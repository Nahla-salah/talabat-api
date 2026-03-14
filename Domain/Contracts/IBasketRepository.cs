using Domain.Models.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        // Get Basket By Id
        Task<CustomerBasket> GetBasketAsync(string Id);
        Task<CustomerBasket> CreateOrUpdateBasketAsync(CustomerBasket basket ,TimeSpan timeSpan);
        Task<bool> DeleteBasketAsync(string Id);




    }
}
