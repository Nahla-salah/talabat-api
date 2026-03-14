using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO_s.OrderDto
{
    public class OrderDto
    {


        public string BasketId { get; init; }
        public AddressDto ShippingAddress { get; init; }
        public int DeliveryMethodId { get; init; }
    }
}
