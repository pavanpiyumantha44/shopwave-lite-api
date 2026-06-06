using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWaveLite.Api.Models.DTOs.Order
{
    public class CreateOrderRequestDto
    {
        public List<OrderItemRequestDto> Items {get; set;} = new();
    }
    public class OrderItemRequestDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}