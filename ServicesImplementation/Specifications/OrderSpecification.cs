using Domain.Models.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesImplementation.Specifications
{
    public class OrderSpecification :BaseSpecifications<Order,Guid>
    {

        public OrderSpecification(string email):base( o => o.UserEmail ==email)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.OrderItems);



        }

        public OrderSpecification(Guid id) : base(o => o.Id ==id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.OrderItems);



        }



    }
}
