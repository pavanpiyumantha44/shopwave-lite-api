using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWaveLite.Api.Models.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; } = true;

        //Foreign key to User (Vendor)
        public Guid VendorId { get; set; }
        public User Vendor { get; set; } = null!;

        //Navigation

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}