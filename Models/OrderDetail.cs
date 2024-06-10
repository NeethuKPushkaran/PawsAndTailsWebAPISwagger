﻿using System.ComponentModel.DataAnnotations;

namespace PawsAndTailsWebAPISwagger.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product product { get; set; }

        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, 10000.00)]
        public decimal UnitPrice { get; set; }

        public Product Product { get; set; }
    }
}
