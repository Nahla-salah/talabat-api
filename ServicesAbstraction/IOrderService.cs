using Shared.DTO_s.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IOrderService
    {

        //Create Order
        //take BasketId ,ShippingAddress ,DeliveryMethodId ,User Email ==>OrderDto
        //Return Order Detatils( Id ,UserName ,OrderDate ,Items(ProductName ,Pict ure Url ,Price ,Quantity),
        //Address ,Delivery MeThod Name ,Order sTATUs Value Sub Total , Total Price)
       
        //Get Order By Id take Order Id ===> Return Order Detatils( Id ,UserName ,OrderDate ,Items(ProductName ,Picure Url ,Price ,Quantity),
       Task<OrderToReturnDto> GetOrderByIdAsync(Guid id);
        
        
        //Get all Orders By Email take User Email ===> Return List of Order Detatils( Id ,UserName ,OrderDate ,Items(ProductName ,Picure Url ,Price ,Quantity),
        Task<IEnumerable<OrderToReturnDto>> GetOrdersForUserAsync(string email);


        //Create Order take BasketId ,ShippingAddress ,DeliveryMethodId ,User Email
        Task<OrderToReturnDto> CreateOrderAsync(OrderDto order, String UserEmail);

        //Get Delivery Methods ==> Return List of Delivery Methods (Id ,Name ,Price)
        Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync();


    }
}
