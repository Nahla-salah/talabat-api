using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO_s.OrderDto
{
    public class OrderItemDto
    {

       
        public string ProductName { get; init; }
        public string PictureUrl { get; init; }
        public decimal Price { get; init; }
        public int Quantity { get; init; }
    }
}
