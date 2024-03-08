using System;
using System.ComponentModel.DataAnnotations;

namespace APG_CRM.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }

        public bool Completed { get; set; } = false;

        // list of order items
        public IList<OrderItem> Items { get; set; } = new List<OrderItem>();

        // one-to-one relationship, where an Order is associated with an enquiry
        // FK to Quotation (one-to-one, Order is dependent)
        public int QuotationId { get; set; }
        public Quotation Quotation { get; set; } = null!;

        // FK to Invoice (one-to-one, Order is dependent)
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        // Define the CustomerId property as the foreign key for the Customer relationship
        public int CustomerId { get; set; }

        // Navigation property to access the associated Customer
        public Customer Customer { get; set; }
    }
}