using Shared.DTO_s.BasketModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasketAsync(string id);

        Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket);

        Task<bool> DeleteBasketAsync(string id);

    }
}
