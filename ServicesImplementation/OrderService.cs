using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.OrderModule;
using Domain.Models.ProductModule;
using ServicesAbstraction;
using ServicesImplementation.Specifications;
using Shared.DTO_s.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesImplementation
{
    public class OrderService(IMapper _mapper ,IBasketRepository _basketRepository ,IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto order, string UserEmail)
        {
            //Map AddressDto to OrderAddress
            var orderAddress = _mapper.Map<AddressDto, OrderAddress>(order.ShippingAddress);
            //Get Basket from BasketId
            var Basket = await _basketRepository.GetBasketAsync(order.BasketId);
            if (Basket == null) return null;
            //Create OrderItemsList from BasketItems
      
            List<OrderItem> orderItems = [];
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var item in Basket.Items)
            {
                var product = await ProductRepo.GetProductById(item.Id);
                if (product == null) throw new ProductNotFoundException(item.Id);
                var itemOrdered = new ProductItemOrder()
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    PictureUrl = product.PictureUrl
                };
                var orderItem = new OrderItem
                {
                    ItemOrdered = itemOrdered,
                    Price = item.Price,
                    Quantity = item.Quantity
                };
                orderItems.Add(orderItem);
            }

            //get DeliveryMethod 
            var DeliveryMethodRepo = _unitOfWork.GetRepository<DeliveryMethod, int>();
            var deliveryMethod = await DeliveryMethodRepo.GetProductById(order.DeliveryMethodId) ?? throw new DeliveryMethodNotFoundException(order.DeliveryMethodId);

            //Calculate Subtotal
            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);
            var newOrder = new Order
            {
                UserEmail = UserEmail,
                ShippingAddress = orderAddress,
                DeliveryMethod = deliveryMethod,
                OrderItems = orderItems,
                Subtotal = subtotal
            };
       await _unitOfWork.GetRepository<Order, Guid>().AddAsync(newOrder);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result <= 0) return null;

            //Map Order to OrderToReturnDto
            return _mapper.Map<Order, OrderToReturnDto>(newOrder);
           

        }

        public async Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
        {
            var DeliveryMethod =await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod> , IEnumerable<DeliveryMethodResult>>(DeliveryMethod);
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(Guid id)
        {
            var spec = new OrderSpecification(id);
            var order =await _unitOfWork.GetRepository<Order, Guid>().GetProductById(spec);
            return _mapper.Map<Order, OrderToReturnDto>(order);
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetOrdersForUserAsync(string email)
        {
            var spec = new OrderSpecification(email);

            var orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDto>>(orders);
        }
    }
}
