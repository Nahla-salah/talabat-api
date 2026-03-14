using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO_s.OrderDto
{
    public record OrderToReturnDto
    {

        public Guid Id { get; init; }
        public string UserEmail {get; init; }
        public string OrderStatus { get; init; } = string.Empty;
        public string DeliveryMethod { get; init; } = string.Empty;
  
        public AddressDto ShippingAddress { get; init; }
        public ICollection<OrderItemDto> OrderItems { get; init; } = [];
            public decimal SubTotal { get; init; }
            public decimal Total { get; init; } // Total = SubTotal + DeliveryPrice
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;






    }
}
