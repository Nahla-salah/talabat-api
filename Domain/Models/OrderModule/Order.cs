using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModule
{

    
    public class Order:BaseEntity<Guid>
    {

        public Order()
        {
                
        }
        public Order(string userEmail, OrderAddress shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> orderItems, decimal subtotal)
        {
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            Subtotal = subtotal;
        }

        public string UserEmail { get; set; }
        public OrderAddress ShippingAddress { get; set; }
      
        public DeliveryMethod DeliveryMethod { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public decimal Subtotal { get; set; }


        public int DeliveryMethodId { get; set; }   // Foreign key for DeliveryMethod

        public decimal GetTotal() => Subtotal + DeliveryMethod.Price;
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public OrderStatus Status { get; set; }

    }
}
