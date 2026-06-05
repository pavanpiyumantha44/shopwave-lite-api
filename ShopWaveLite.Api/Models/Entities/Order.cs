using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWaveLite.Api.Models.Entities;

    public enum OrderStatus
    {
        Pending,
        Confirmed,
        Shipped,
        Delivered,
        Cancelled
    }
    public class Order : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public User Customer { get; set; } = null!;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal TotalAmount { get; set; }

        //Navigation
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); 
    }