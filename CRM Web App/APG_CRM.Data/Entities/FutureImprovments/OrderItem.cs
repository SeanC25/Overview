using System;
using System.ComponentModel.DataAnnotations;

namespace APG_CRM.Data.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 100)]
        public int ItemQuantity { get; set; }

        [Required]
        [Range(0, 4000)]
        public decimal SizeA { get; set; }

        [Required]
        [Range(0, 4000)]
        public decimal SizeB { get; set; }

        public decimal GlassArea => SizeA * SizeB;

        [Required]
        public bool Shaped { get; set; } = false;


        // relationships
        public int GlassId { get; set; }
        public Glass Item { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        //Classification-


    }
}