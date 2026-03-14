using Domain.Contracts;
using Domain.Models.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistence.Repository
{
    public class BasketRepository(IConnectionMultiplexer connectionMultiplexer) : IBasketRepository
    {
        private readonly IDatabase _database = connectionMultiplexer.GetDatabase();

        public async Task<CustomerBasket> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan timeSpan)
        {
           




            var JsonBasket = JsonSerializer.Serialize(basket);  
          _database.StringSetAsync(basket.Id, JsonBasket, timeSpan);
            if (JsonBasket == null)
            {
                return null;
            }

            return await GetBasketAsync(basket.Id);



        }

     

        public async Task<CustomerBasket> GetBasketAsync(string Id)
        {
         var Basket =  await  _database.StringGetAsync(Id);
            if(Basket.IsNullOrEmpty)
                {
                return null;
            }
            else
                return JsonSerializer.Deserialize<CustomerBasket>(Basket);

        }

        public async Task<bool> DeleteBasketAsync(string Id)
        {

            return await _database.KeyDeleteAsync(Id);
        }
    }
}
