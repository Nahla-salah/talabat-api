using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DTO_s.BasketModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{

    [ApiController]
    [Route("Api/[Controller]")]
    public class BasketController(IServiceManager serviceManager):ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDto>> GetBasket(string id)
        {
         var Basket =await   serviceManager.BasketService.GetBasketAsync(id);
            return Ok(Basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
         var result =await  serviceManager.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
         var result =await  serviceManager.BasketService.DeleteBasketAsync(id);
            return Ok(result);
        }
}
}
